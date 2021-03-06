﻿using CavesOfUnknowNez.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace CavesOfUnknowNez
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
       // GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;
        
        public Game1() : base(width:1280,height:768,isFullScreen: false,enableEntitySystems: false)

        {
            //graphics = new GraphicsDeviceManager(this);
           // Content.RootDirectory = "Content";
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

            base.Initialize();
            Window.AllowUserResizing = true;
            //var myScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);
            
            //var texture = myScene.content.Load<Texture2D>("Graphics/player");

            // setup our Scene by adding some Entities
           // var entityOne = myScene.createEntity("entity-one");
           // entityOne.addComponent(new Sprite(texture));

           // var entityTwo = myScene.createEntity("entity-two");
           // entityTwo.addComponent(new Sprite(texture));

            // move entityTwo to a new location so it isn't overlapping entityOne
           // entityTwo.transform.position = new Vector2(200, 200);
            //scene = myScene;
            //do butterscene
            var butterScene = new ButterScene();
            butterScene.clearColor = Color.DarkGreen;
            butterScene.addRenderer(new DefaultRenderer(0, null));

            scene = butterScene;


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           // GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
