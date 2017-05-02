using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
        SoundEffect laser;
        Song mainLoop;
        Texture2D playerLives;

        public enum WeaponType { SingleLaser, DualLaser };
        public enum GameState { TitleMenu, GamePlay };

        public static int PLAYAREAX = 600;
        public static int PLAYAREAY = 900;
        public static int SCREENAREAX = 800;
        public static int SCREENAREAY = PLAYAREAY;
        public static float scale = 0.5f;

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

            // TODO: use this.Content to load your game content here
            textFont = Content.Load<SpriteFont>(@"Fonts\kenvector_future");
            projectiles.LoadContent(Content.Load<Texture2D>(@"Graphics\Projectiles\laserRed07"));
            player.LoadContent(Content.Load<Texture2D>(@"Graphics\Player\playerShip1_red"));
            enemies.LoadContent(Content.Load<Texture2D>(@"Graphics\Enemies\enemyBlue1"));
            laser = Content.Load<SoundEffect>(@"Sounds\FX\sfx_laser1");
            mainLoop = Content.Load<Song>(@"Sounds\Music\MainLoop");
            playerLives = Content.Load<Texture2D>(@"Graphics\Player\playerLife1_red");
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
                        if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                            projectiles.FireProjectile(gameTime, player);
                        }
                        starfield.Update(gameTime);
                        enemies.Update(gameTime, player, projectiles);
                        player.Update(gameTime, Keyboard.GetState());
                        projectiles.Update(gameTime, player, enemies.enemies);
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

                        spriteBatch.Begin();

                        starfield.Draw(spriteBatch);
                        enemies.Draw(spriteBatch);
                        projectiles.Draw(spriteBatch);
                        player.Draw(spriteBatch);
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
            player.lives = 3;
            player.score = 0;
#if !DEBUG
            MediaPlayer.Play(mainLoop);
            MediaPlayer.IsRepeating = true;
#endif
        }

        private void DrawStats()
        {
            int lineWidth = textFont.LineSpacing + 5;

            spriteBatch.DrawString(textFont, "Player score:", new Vector2(PLAYAREAX + 10, 4 + lineWidth * 0), Color.White);
            spriteBatch.DrawString(textFont, player.score.ToString(), new Vector2(SCREENAREAX - textFont.MeasureString(player.score.ToString()).X - 2, 4 + lineWidth * 1), Color.White);
            spriteBatch.DrawString(textFont, "High score:", new Vector2(PLAYAREAX + 10, 4 + lineWidth * 2), Color.White);
            spriteBatch.DrawString(textFont, player.highScore.ToString(), new Vector2(SCREENAREAX - textFont.MeasureString(player.highScore.ToString()).X - 2, 4 + lineWidth * 3), Color.White);
            spriteBatch.DrawString(textFont, "Lives:", new Vector2(PLAYAREAX + 10, 4 + lineWidth * 4), Color.White);

            // Draw lives
            // This should probably be in a function :/
            for (int i = player.lives; i >= 1; i--)
            {
                spriteBatch.Draw(playerLives, new Vector2((SCREENAREAX - ((playerLives.Width * Game1.scale + 4) * i)), 4 + lineWidth * 5), null, Color.White, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
            }
        }
    }
}
