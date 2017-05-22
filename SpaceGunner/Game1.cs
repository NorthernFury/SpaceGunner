using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static SpaceGunner.Weapons;

namespace SpaceGunner
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Starfield starfield;
        ProjectileManager projectiles;
        EnemyManager enemies;
        SpriteFont textFont;
        sfxManager soundEffects;
        Song mainLoop;
        Texture2D playerLives;
        KeyboardState currentKeyState, previousKeyState;

        public enum GameState { TitleMenu, GamePlay };

        public static int PLAYAREAX = 600;
        public static int PLAYAREAY = 900;
        public static int SCREENAREAX = 800;
        public static int SCREENAREAY = PLAYAREAY;
        public static float scale = 0.6f;

        public GameState gameState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREENAREAX;
            graphics.PreferredBackBufferHeight = SCREENAREAY;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            starfield = new Starfield();
            projectiles = new ProjectileManager();
            enemies = new EnemyManager();
            soundEffects = new sfxManager();

            gameState = GameState.TitleMenu;
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textFont = Content.Load<SpriteFont>(@"Fonts\kenvector_future");
            player.SetTexture(Content.Load<Texture2D>(@"Graphics\Player\playerShip1_blue"));
            enemies.LoadContent("red", Content.Load<Texture2D>(@"Graphics\Enemies\enemyRed1"));
            enemies.LoadContent("explosion", Content.Load<Texture2D>(@"Graphics\Particles\explosion-6"));
            playerLives = Content.Load<Texture2D>(@"Graphics\Player\playerLife1_blue");

            // Laser textures
            projectiles.LoadContent("red", Content.Load<Texture2D>(@"Graphics\Projectiles\laserRed07"));
            projectiles.LoadContent("blue", Content.Load<Texture2D>(@"Graphics\Projectiles\laserBlue07"));
            projectiles.LoadContent("green", Content.Load<Texture2D>(@"Graphics\Projectiles\laserGreen13"));

            // Sounds & music
            soundEffects.LoadContent("laser1", Content.Load<SoundEffect>(@"Sounds\FX\sfx_laser1"));
            soundEffects.LoadContent("enemyexplosion", Content.Load<SoundEffect>(@"Sounds\FX\enemyexplosion"));
            soundEffects.LoadContent("powerup", Content.Load<SoundEffect>(@"Sounds\FX\powerup"));
            mainLoop = Content.Load<Song>(@"Sounds\Music\MainLoop");
            starfield.LoadContent(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.TitleMenu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        NewGame();
                        gameState = GameState.GamePlay;
                    }
                    break;
                case GameState.GamePlay:
                    if (player.crashed)
                    {
                        player.ResetPlayer();
                        if (player.lives < 0)
                        {
                            // TODO: Add a game over screen to display with a resart option
                            gameState = GameState.TitleMenu;
                            player.highScore = player.score;
                            MediaPlayer.Stop();
                        }
                    }
                    else
                    {
                        ProcessInput(gameTime, Keyboard.GetState());
                        starfield.Update(gameTime);
                        enemies.Update(gameTime, player, projectiles, soundEffects);
                        player.Update(gameTime);
                        projectiles.Update(gameTime, player);
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.TitleMenu:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();
                    spriteBatch.End();
                    break;
                case GameState.GamePlay:
                    if (player.crashed)
                    {

                    }
                    else
                    {
                        GraphicsDevice.Clear(Color.Black);

                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                        starfield.Draw(spriteBatch);
                        enemies.Draw(spriteBatch);
                        player.Draw(spriteBatch);
                        projectiles.Draw(spriteBatch);
                        DrawStats();

                        spriteBatch.End();
                    }
                    break;
                default:
                    break;
            }

            base.Draw(gameTime);
        }

        private void NewGame()
        {
            starfield.ResetField();
            enemies.ResetEnemies();
            player.ResetPlayer();
            projectiles.ResetProjectiles();
            player.lives = 3;
            player.score = 0;
#if !DEBUG
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.Play(mainLoop);
            MediaPlayer.IsRepeating = true;
#endif
        }

        private void DrawStats()
        {
            int lineWidth = textFont.LineSpacing + 5;
            int xPadding = PLAYAREAX + 10;

            spriteBatch.DrawString(textFont, "Player score:", new Vector2(xPadding, 4 + lineWidth * 0), Color.White);
            spriteBatch.DrawString(textFont, player.score.ToString(), new Vector2(SCREENAREAX - textFont.MeasureString(player.score.ToString()).X - 2, 4 + lineWidth * 1), Color.White);
            spriteBatch.DrawString(textFont, "High score:", new Vector2(xPadding, 4 + lineWidth * 2), Color.White);
            spriteBatch.DrawString(textFont, player.highScore.ToString(), new Vector2(SCREENAREAX - textFont.MeasureString(player.highScore.ToString()).X - 2, 4 + lineWidth * 3), Color.White);
            spriteBatch.DrawString(textFont, "Lives:", new Vector2(xPadding, 4 + lineWidth * 4), Color.White);

            // Draw lives indicator
            // This should probably be in a function?
            for (int i = player.lives; i >= 1; i--)
            {
                spriteBatch.Draw(playerLives, new Vector2((SCREENAREAX - ((playerLives.Width * scale + 4) * i)), 4 + lineWidth * 5), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

            spriteBatch.DrawString(textFont, "Weapon:", new Vector2(xPadding, 4 + lineWidth * 6), Color.White);
            spriteBatch.DrawString(textFont, player.equippedWeapon.name, new Vector2(SCREENAREAX - textFont.MeasureString(player.equippedWeapon.name).X - 2, 4 + lineWidth * 7), Color.White);
        }

        private void ProcessInput(GameTime gameTime, KeyboardState keyState)
        {
            Vector2 direction = new Vector2(0, 0);
            currentKeyState = keyState;

            if (keyState.IsKeyDown(Keys.Space))
            {
                player.equippedWeapon.Fire(gameTime, projectiles, soundEffects.Effect("laser1"), null);
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                direction.X = 1;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                direction.Y = 1;
            }

            player.ChangeDirection(direction);

            if (currentKeyState.IsKeyDown(Keys.Z) && previousKeyState.IsKeyUp(Keys.Z))
            {
                // toggle equipped weapons
                switch (player.equippedWeapon.weapon)
                {
                    case WeaponType.SingleLaser:
                        player.equippedWeapon.changeWeapon(WeaponType.DualLaser);
                        break;
                    case WeaponType.DualLaser:
                        player.equippedWeapon.changeWeapon(WeaponType.SpreadShot);
                        break;
                    case WeaponType.SpreadShot:
                        player.equippedWeapon.changeWeapon(WeaponType.SingleLaser);
                        break;
                    default:
                        break;
                }
            }

            previousKeyState = keyState;
        }
    }
}
