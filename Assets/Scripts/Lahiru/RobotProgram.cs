using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotProgram
{
	#region Program Commands
	public interface Command
	{
		IEnumerator Execute(GameObject gameObject);
	}

	public float speed = 1.0f;  // speed of movement
	private Vector3 forward, right;  // vectors for forward and right directions
	public bool isJumping = false;  // flag for jump action
	private bool isMoving = true; // initialize as true to enable movement
	public bool isPickup = false;
	//public GameObject bomb = GameObject.FindGameObjectWithTag("Bomb");

	/*
	public bool CheckCondition(GameObject gameObject)
	{
		// Define the raycast parameters
		float y = 1.5f;  // the desired y-coordinate
		Vector3 raycastOrigin = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
		Vector3 raycastDirection = Vector3.forward;
		float maxDistance = 1.0f;

		// Perform the raycast
		RaycastHit hit;
		if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, maxDistance))
		{
			if (hit.collider.CompareTag("Obstacle"))
			{
				Debug.Log("Obstacle detected in front of game object!");
				return false;
			}
			else if (hit.collider.CompareTag("Star"))
			{
				Debug.Log("Star detected in front of game object!");
				hit.collider.gameObject.SetActive(false);
			}
		}
		Debug.Log(raycastOrigin + " iooooo");
		return true;
	}
	*/

	public class MoveCommand :  Command
	{
		public enum Direction
		{
			FORWARD, BACKWARD, LEFT, RIGHT
		}

		private Direction _direction;
		private float _distance = 1f;

		public MoveCommand(Direction direction)
		{
			_direction = direction;
		}

		public bool PerformRaycast(Vector3 raycastOrigin, Vector3 direction, float maxDistance)
		{
			RaycastHit hit;
			if (Physics.Raycast(raycastOrigin, direction, out hit, maxDistance))
			{
				if (hit.collider.CompareTag("Obstacle"))
				{
					Debug.Log("Obstacle detected in front of game object!");
					return false;
				}
                else if (hit.collider.CompareTag("Bomb"))
                {
                    Debug.Log("Bomb detected in front of game object!");
                    return false;
                }
                else if (hit.collider.CompareTag("Star"))
				{
					Debug.Log("Star detected in front of game object!");
					hit.collider.gameObject.SetActive(false);
				}
			}
			return true;
		}

		public IEnumerator Execute(GameObject gameObject)
		{
			float d = 1.0f;
            Vector3 v = Vector3.zero;


		PlayerController[] _robots = GameObject.FindObjectsOfType<PlayerController>();

			if (_direction == Direction.FORWARD)
            {

				gameObject.transform.forward = Vector3.forward;
				float y = 1.5f;  // the desired y-coordinate
				Vector3 raycastOrigin = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);  // create a new position with the desired y-coordinate
				bool obstaclesDetected = PerformRaycast(raycastOrigin, Vector3.forward, 1.0f);
				Debug.Log(raycastOrigin + " Forward!");
				if (obstaclesDetected)
				{
					gameObject.transform.position += Vector3.forward * d;
				}


			}
            else if (_direction == Direction.BACKWARD)
            {

				gameObject.transform.forward = (-Vector3.forward);
				float y = 1.5f;  // the desired y-coordinate
				Vector3 raycastOrigin = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);  // create a new position with the desired y-coordinate
				bool obstaclesDetected = PerformRaycast(raycastOrigin, (-Vector3.forward), 1.0f);
				Debug.Log(raycastOrigin + " Backword!");
				if (obstaclesDetected)
				{
					gameObject.transform.position += (-Vector3.forward) * d;
				}

			}
            else if (_direction == Direction.LEFT)
            {

				gameObject.transform.forward = (-Vector3.right);
				float y = 1.5f;  // the desired y-coordinate
				Vector3 raycastOrigin = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);  // create a new position with the desired y-coordinate
				bool obstaclesDetected = PerformRaycast(raycastOrigin, (-Vector3.right), 1.0f);
				Debug.Log(raycastOrigin + " LEFT!");
				if (obstaclesDetected)
				{
					gameObject.transform.position += (-Vector3.right) * d;
				}

			}
            else if (_direction == Direction.RIGHT)
            {
				

				gameObject.transform.forward = Vector3.right;

				float y = 1.5f;  // the desired y-coordinate
				Vector3 raycastOrigin = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);  // create a new position with the desired y-coordinate
				bool obstaclesDetected = PerformRaycast(raycastOrigin, Vector3.right, 1.0f);
				Debug.Log(raycastOrigin + " RIGHT!");
				if (obstaclesDetected)
				{
					gameObject.transform.position += Vector3.right * d;
				}
			}
			return null;
		}
	}

	public class PickCommand : Command
	{
		Vector3 _direction;
		public PickCommand(Vector3 pickDirection)
		{
			_direction  = pickDirection;
		}

		public int PerformRaycast(Vector3 raycastOrigin, Vector3 direction, float maxDistance)
		{
			RaycastHit hit;
			if (Physics.Raycast(raycastOrigin, direction, out hit, maxDistance))
			{
				if (hit.collider.CompareTag("Obstacle"))
				{
					Debug.Log("Obstacle detected in front of game object!");
					return 1;
				}
				else if (hit.collider.CompareTag("Star"))
				{
					Debug.Log("Star detected in front of game object!");
					hit.collider.gameObject.SetActive(false);
					return 2;
				}
				else if (hit.collider.CompareTag("Bomb"))
				{
					Debug.Log("Bomb detected in front of game object!");
					return 3;
				}
			}
			return 5;
		}

		public IEnumerator Execute(GameObject gameObject)
		{
			GameObject bomb = GameObject.FindGameObjectWithTag("Bomb");
			float y = 1.5f;  // the desired y-coordinate
			Vector3 raycastOrigin = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);  // create a new position with the desired y-coordinate
			int obstaclesDetected = PerformRaycast(raycastOrigin, _direction, 1.0f);
			if (obstaclesDetected == 3)
			{
				

				// Disable the collider component of the bomb

				Collider bombCollider = bomb.GetComponent<Collider>();
				bombCollider.enabled = false;

				Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
				bombRigidbody.useGravity = false;

				bombRigidbody.constraints = RigidbodyConstraints.FreezeAll;

				bomb.transform.parent = gameObject.transform; // Make the bomb a child of the player
				bomb.transform.localPosition = new Vector3(0f, 1f, 1f); // Adjust the position of the bomb relative to the player
			}

			return null;
		}
	}

	public class DropCommand : Command
	{


		public DropCommand()
		{

		}

		public IEnumerator Execute(GameObject gameObject)
		{
			GameObject bomb = GameObject.FindGameObjectWithTag("Bomb");
			//Vector3 dropPosition = transform.position + (transform.forward * 1.0f);
			Vector3 dropPosition = gameObject.transform.position + new Vector3(0f, 0.5f, 0f) + (gameObject.transform.forward * 1.0f);
			bomb.transform.parent = null; // Detach the bomb from the player

			Collider bombCollider = bomb.GetComponent<Collider>();
			bombCollider.enabled = true;

			// Add a rigidbody component to the bomb and activate gravity
			Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
			bombRigidbody.useGravity = true;

			bombRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

			bomb.transform.position = dropPosition; // Set the bomb's position to the drop position

			return null;
		}
	}

	public class RotateCommand : Command
	{
		private float _angle;

		public RotateCommand(float angle)
		{
			_angle = angle;
		}

		public IEnumerator Execute(GameObject gameObject)
		{
			gameObject.transform.Rotate(new Vector3(0, 0, _angle));

			#if DEBUG
			Debug.Log ("Rotate " + _angle);
			#endif

			return null;
		}
	}
	#endregion

	public class JumpCommand : Command
    {

		private Vector3 _direction;
		public JumpCommand(Vector3 direction)
        {
			_direction = direction;

		}

		public IEnumerator Execute(GameObject gameObject)
		{

			PlayerController[] _robots = GameObject.FindObjectsOfType<PlayerController>();



			//isJumping = true;
			gameObject.transform.forward = _direction;
			_direction.y = 0;
			_direction = Vector3.Normalize(_direction);

			float jumpHeight = 3.0f;  // Height of the jump
			float jumpDistance = 2.0f;  // Maximum distance to move forward during the jump
			float jumpDuration = 1.0f;  // Duration of the jump

			float gravity = (2 * jumpHeight) / (jumpDuration * jumpDuration);  // Calculate the gravity based on the desired jump height and duration

			// Calculate the initial upward velocity
			float initialUpwardVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);  // Calculate the initial velocity required for the desired jump height

			// Calculate the time taken to reach the apex of the jump
			float timeToApex = initialUpwardVelocity / gravity;

			// Calculate the time taken for the downward motion
			float timeToGround = jumpDuration - timeToApex;

			// Calculate the upward motion
			float upwardDistance = initialUpwardVelocity * timeToApex - (0.5f * gravity * timeToApex * timeToApex);  // Distance covered during the upward motion

			// Calculate the maximum forward motion distance for the jump
			float maxForwardDistance = jumpDistance;

			// Calculate the forward motion distance for the upward motion
			float upwardForwardDistance = (maxForwardDistance / 2.0f);

			// Calculate the forward motion distance for the downward motion
			float downwardForwardDistance = maxForwardDistance - upwardForwardDistance;

			// Calculate the duration for moving up and coming back down
			float halfDuration = jumpDuration / 2.0f;

			// Move the player up using Lerp for the first half duration
			Vector3 apexPosition = gameObject.transform.position + (_direction * upwardForwardDistance) + (Vector3.up * upwardDistance / 2.0f);
			//yield return StartCoroutine(LerpJump(gameObject.transform.position, apexPosition, halfDuration));
			float timer = 0f;
			while (timer < 0.3*halfDuration)
			{
				timer += Time.deltaTime;
				gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, apexPosition, halfDuration);
				yield return null;
			}


			// Move the player back down to the ground using a separate Lerp for the remaining half duration
			Vector3 groundPosition = gameObject.transform.position + (_direction * downwardForwardDistance) - (Vector3.up * upwardDistance / 2.0f);
			//yield return StartCoroutine(LerpJump(apexPosition, groundPosition, halfDuration));

			while (timer < 0.3*halfDuration)
			{
				timer += Time.deltaTime;
				gameObject.transform.position = Vector3.Lerp(apexPosition, groundPosition, halfDuration);
				yield return null;
			}

			// Check for collision with obstacles at the target position
			Collider[] collidingObjects = Physics.OverlapSphere(groundPosition, 0.5f);  // Use an overlap sphere with a radius of 0.5 (adjust as needed)

			foreach (Collider collidingObject in collidingObjects)
			{
				if (collidingObject.CompareTag("Obstacle"))
				{
					Debug.Log("Obstacle detected at the landing position. Game Over!");
				}
                if (collidingObject.GetComponent<Collider>().CompareTag("Bomb"))
                {
                    Debug.Log("Bomb detected at the landing position. Game Over!");
                }
                if (collidingObject.CompareTag("Star"))
				{
					Debug.Log("Star detected at the landing position.!");
					collidingObject.gameObject.SetActive(false);
					//Destroy(collidingObject.gameObject);
					//return null;
				}
			}

			gameObject.transform.position = groundPosition; // Ensure the player reaches the exact ground position

			//isJumping = false;  // Set isJumping to false once the jump is complete

		}

	}
	


	public static float EXECUTION_DELAY = .5f;

	private List<Command> _commands;
	private int _pc;

	public RobotProgram(List<Command> commands)
	{
		_commands = commands;
		_pc = 0;
	}


	public IEnumerator Run(GameObject gameObject)
	{
		while(_pc < _commands.Count)
		{
			yield return new WaitForSeconds(EXECUTION_DELAY);

			Command nextCommand = _commands[_pc++];
			yield return nextCommand.Execute(gameObject);
		}
	}


	public void Reset()
	{
		_pc = 0;
	}
}
