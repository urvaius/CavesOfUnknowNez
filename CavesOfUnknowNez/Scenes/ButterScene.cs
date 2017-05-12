using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CavesOfUnknowNez.Entities;
using CavesOfUnknowNez.Scenes;
using static Nez.Scene;
using Nez.Tiled;

namespace CavesOfUnknowNez.Scenes
{
	public class ButterScene : Scene
	{


				
				public ButterScene()
				{
			
			

				}

		
		public override void initialize()
		{
			base.initialize();

			//load caveman stuff and try
			#region caveman
			setDesignResolution(640, 480, Scene.SceneResolutionPolicy.ShowAllPixelPerfect);
			Screen.setSize(640 * 2, 480 * 2);

			var tiledEntity = createEntity("tiled-map-entity");
			var tiledMap = content.Load<TiledMap>(Content.Platformer.tiledMap);
			var objectLayer = tiledMap.getObjectGroup("objects");
			var spawnObject = objectLayer.objectWithName("spawn");

			var tiledMapComponent = tiledEntity.addComponent(new TiledMapComponent(tiledMap, "main"));

			var playerEntity = createEntity("player", new Vector2(spawnObject.x, spawnObject.y));
			playerEntity.addComponent(new Player());
			playerEntity.addComponent(new BoxCollider(-8, -16, 16, 32));
			playerEntity.addComponent(new TiledMapMover(tiledMapComponent.collisionLayer));
			//addPostProcessor(new VignettePostProcessor(1));




			#endregion





			//var myScene = Scene.createWithDefaultRenderer(Color.Black);

			initiatePlayer();

			

			//scene = myScene;

			}

		private void initiatePlayer()
		{
			//var playerTexture = content.Load<Texture2D>("Graphics/player");

			
			
			//var playerOne = createEntity("Player1");
			//playerOne.addComponent(new Sprite(playerTexture));

			////add movement simple mover
			//playerOne.addComponent(new SimpleMover());
			//playerOne.addComponent(new DamageComponent());

			//playerOne.transform.position = new Vector2(200, 200);
		}
	}
}
