using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez.Sprites;

namespace CavesOfUnknowNez
{
	// first off, we subclass Component. We want to get update calls every frame so we add the IUpdatable interface
	// (which just consists of the update method).
	public class SimpleMover : Component, IUpdatable
	{
		public float _speed = 600f;
		Mover _mover;
		Sprite _sprite;



		public override void onAddedToEntity()
		{
			_sprite = this.getComponent<Sprite>();
			_mover = new Mover();
			entity.addComponent(_mover);

		}
		void IUpdatable.update()
		{
			var moveDir = Vector2.Zero;

			// Input provides access to the keyboard here. We check for the left/right/up/down arrow keys and set the movement direction accordingly.
			if (Input.isKeyDown(Keys.Left ) || Input.isKeyDown(Keys.A)) 
				moveDir.X = -1f;
			if (_sprite != null)
				_sprite.flipX = true;
			else if (Input.isKeyDown(Keys.Right) || Input.isKeyDown(Keys.D))
				moveDir.X = 1f;
			if (_sprite != null)
				_sprite.flipX = false;

			if (Input.isKeyDown(Keys.Up)||Input.isKeyDown(Keys.W))
				moveDir.Y = -1f;
				
			else if (Input.isKeyDown(Keys.Down)|| Input.isKeyDown(Keys.S))
				moveDir.Y = 1f;

			if(moveDir != Vector2.Zero)
			{
				var movement = moveDir * _speed * Time.deltaTime;
				CollisionResult res;
				if(_mover.move(movement,out res))
				{
					Debug.drawLine(entity.transform.position, entity.transform.position + res.normal * 100, Color.Black, 0.3f);

				}
			}

			// every Entity has a transform property. The transform defines the Entity's physical representation in space (position/rotation/scale).
			// here we are just modifying the position to move the Entity around. We multiply the movement by Time.deltaTime to keep things
			// framerate independent.
			entity.transform.position += moveDir * _speed * Time.deltaTime;
		}
	}
}
