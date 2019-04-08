using UnityEngine;
using System.Collections;

public class level_generation : MonoBehaviour
{
    static level_generation Instance = null;

    ArrayList TheShitISpawned = new ArrayList();

	struct BetterRoom
	{
		public int left;
		public int right;
		public int top;
		public int bottom;

		public bool IsOverlapping(BetterRoom other)
		{
			return (this.left <= other.right && this.right >= other.left && this.top <= other.bottom && this.bottom >= other.top);
        }
	}

    System.Random random_generator;

    public int room_count = 100;
    public int room_radius = 500;
    public int room_width_min = 25;
    public int room_width_max = 50;
    public int room_height_min = 25;
    public int room_height_max = 50;
    public float room_ratio_min = 0.5f;
    public float room_ratio_max = 1.5f;

	public int blocking_1_in = 50;
    
    int world_left = int.MaxValue;
    int world_right = int.MinValue;
    int world_top = int.MaxValue;
    int world_bottom = int.MinValue;

    int world_width = 0;
    int world_height = 0;

	int world_portal_x = -3;
	int world_protal_y = 0;

    GameObject[,] world_map;
	GameObject[] blocking_objects;
	GameObject portal;
	int blocking_iter = 0;

    bool b = true;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        this.random_generator = new System.Random(Time.time.GetHashCode());

        this.world_left = int.MaxValue;
        this.world_right = int.MinValue;
        this.world_top = int.MaxValue;
        this.world_bottom = int.MinValue;

        this.world_width = 0;
        this.world_height = 0;

		this.world_map = null;

    }

    // Update is called once per frame
    void Update()
    {
		if (GameManager.GetGameManager().debug_commands)
		{
			if (Input.GetKeyDown(KeyCode.S))
			{
				string[] sets = { "desert", "evil", "grass", "mystery", "rock" };
				this.GenerateBetterMap(GameManager.GetGameManager().GetLevelDef(sets[this.random_generator.Next(sets.Length)]));
			}
		}
    }

    public static level_generation GetInstance()
    {
        return Instance;
    }

    bool TranslateWorldToArray(int in_x, int in_y, ref int out_x, ref int out_y)
    {
        // Add half width and height to input for output variables.
        out_x = in_x + world_width / 2;
        out_y = in_y + world_height / 2;
		
		// Return if the out position is actually within the array space.
		return (out_x >= 0 && out_x < this.world_width && out_y >= 0 && out_y < this.world_height);
    }

    public bool IsTraversible(float position_x, float position_y)
    {
        int world_x = (int)(position_x / GameManager.GetGameManager().TileSize);
		int world_y = (int)(position_y / GameManager.GetGameManager().TileSize);

        return (this.IsTraversible(world_x, world_y));
	}

	bool IsTraversible(int position_x, int position_y)
	{
		bool out_bool = false;

		int out_x = 0, out_y = 0;
		if (this.TranslateWorldToArray(position_x, position_y, ref out_x, ref out_y))
		{
			if (this.world_map[out_x, out_y] != null)
			{
				if (this.world_map[out_x, out_y].GetComponent<tile>().IsTraversible())
				{
					out_bool = true;
				}
			}
		}

		return (out_bool);
	}

	bool IsTraversible_array(int position_x, int position_y)
	{
		bool out_bool = false;
		
		if (position_x >= 0 && position_x < this.world_width && position_y >= 0 && position_y < this.world_height)
		{
			if (this.world_map[position_x, position_y] != null)
			{
				if (this.world_map[position_x, position_y].GetComponent<tile>().IsTraversible())
				{
					out_bool = true;
				}
			}
		}

		return (out_bool);
	}

	bool IsNull(int position_x, int position_y)
	{
		bool out_bool = true;

		int out_x = 0, out_y = 0;
		if (this.TranslateWorldToArray(position_x, position_y, ref out_x, ref out_y))
		{
			if (this.world_map[out_x, out_y] != null)
			{
				out_bool = false;
			}
		}

		return (out_bool);
	}

	bool IsNull_array(int position_x, int position_y)
	{
		bool out_bool = true;

		if (position_x >= 0 && position_x < this.world_width && position_y >= 0 && position_y < this.world_height)
		{
			if (this.world_map[position_x, position_y] != null)
			{
				out_bool = false;
			}
		}

		return (out_bool);
	}

	public void GenerateBetterMap(level_def def)
	{
        for( int i = 0; i < TheShitISpawned.Capacity; ++i )
        {
            DestroyObject( (GameObject)TheShitISpawned[i] );
        }
        TheShitISpawned.Clear();

		if (def == null)
		{
			return;
		}

		string tile_set = def.tile_set[random_generator.Next(def.tile_set.Length)];

		if (this.world_map != null)
		{
			foreach (GameObject go in this.world_map)
			{
				Destroy(go);
				this.world_map = null;
			}
		}

		if (this.blocking_objects != null)
		{
			foreach (GameObject go in this.blocking_objects)
			{
				Destroy(go);
			}
		}

		if (this.portal != null)
		{
			Destroy(this.portal);
		}

		this.blocking_objects = new GameObject[1000];
		this.blocking_iter = 0;

		int tile_count = 0;

		BetterRoom[] rooms = new BetterRoom[this.room_count];
		
		
		// Iterate for number of rooms, creating the position and dimensions for each.
		for (int room = 1; room < rooms.Length / 2; room++)
		{
			int x = 0;
			int y = this.random_generator.Next(-this.room_radius, this.room_radius);

			int width = 0;
			int height = 0;

			//Generate the dimensions of room.
			do
			{
				width = this.random_generator.Next(this.room_width_min, this.room_width_max);
				height = this.random_generator.Next(this.room_height_min, this.room_height_max);
			}
			while (width / height <= room_ratio_max && width / height >= room_ratio_min);

			rooms[room].left = x - width;
			rooms[room].right = x + width;
			rooms[room].top = y - height;
			rooms[room].bottom = y + height;



			// Check if the top of the room is further than world top.
			// True - Update world top to be room top.
			if (rooms[room].top < this.world_top)
			{
				this.world_top = rooms[room].top;
			}

			// Check if the bottom of the room is further than world bottom.
			// True - Update world bottom to be room bottom.
			if (rooms[room].bottom > this.world_bottom)
			{
				this.world_bottom = rooms[room].bottom;
			}
		}

		for (int i = 0; i < rooms.Length; i++)
		{
			bool move_left = this.random_generator.Next(2) == 0;

			while (this.IsOverlapping(i, ref rooms))
			{
				rooms[i].left += move_left ? -1 : 1;
				rooms[i].right += move_left ? -1 : 1;
			}

			// Check if the left of the room is further than world left.
			// True - Update world left to be room left.
			if (rooms[i].left < this.world_left)
			{
				this.world_left = rooms[i].left;
			}

			// Check if the right of the room is further than world right.
			// True - Update world right to be room right.
			if (rooms[i].right > this.world_right)
			{
				this.world_right = rooms[i].right;
			}
		}

		// Iterate for number of rooms, creating the position and dimensions for each.
		for (int room = rooms.Length / 2; room < rooms.Length; room++)
		{
			int x = this.random_generator.Next(-this.room_radius, this.room_radius);
			int y = this.random_generator.Next(-this.room_radius, this.room_radius);

			int width = this.random_generator.Next(this.room_width_min, this.room_width_max);
			int height = this.random_generator.Next(this.room_height_min, this.room_height_max);

			rooms[room].left = x - width;
			rooms[room].right = x + width;
			rooms[room].top = y - height;
			rooms[room].bottom = y + height;

			// Check if the left of the room is further than world left.
			// True - Update world left to be room left.
			if (rooms[room].left < this.world_left)
			{
				this.world_left = rooms[room].left;
			}

			// Check if the right of the room is further than world right.
			// True - Update world right to be room right.
			if (rooms[room].right > this.world_right)
			{
				this.world_right = rooms[room].right;
			}

			// Check if the top of the room is further than world top.
			// True - Update world top to be room top.
			if (rooms[room].top < this.world_top)
			{
				this.world_top = rooms[room].top;
			}

			// Check if the bottom of the room is further than world bottom.
			// True - Update world bottom to be room bottom.
			if (rooms[room].bottom > this.world_bottom)
			{
				this.world_bottom = rooms[room].bottom;
			}
		}

		// Ensure center is filled
		rooms[0].left = -5;
		rooms[0].right = 5;
		rooms[0].top = -5;
		rooms[0].bottom = 5;

		// Calculate the actual width and height of the world.
		this.world_width = this.world_right - this.world_left;
		this.world_height = this.world_bottom - this.world_top;


		// Create array for storage of game tiles.
		this.world_map = new GameObject[this.world_width, this.world_height];

		GameObject tile_flat = level_assets.GetInstance().GetTileFromSet(tile_set, "flat");
		GameObject tile_left = level_assets.GetInstance().GetTileFromSet(tile_set, "left");
		GameObject tile_right = level_assets.GetInstance().GetTileFromSet(tile_set, "right");

		// Iterate over rooms in generated list.
		foreach (BetterRoom room in rooms)
		{
			// Iterate over the x indexes of the current room.
			for (int x = room.left; x <= room.right; x++)
			{
				// Iterate over the y indexes of the current room.
				for (int y = room.top; y <= room.bottom; y++)
				{
					// Initiliase the index variables for translation.
					int index_x = 0;
					int index_y = 0;

					// Check that the world space was succesfully translated to an array indexer.
					// True - Set the type of the title on the world map to be a traversible space.
					if (this.TranslateWorldToArray(x, y, ref index_x, ref index_y))
					{
						if (this.world_map[index_x, index_y] == null)
						{
							this.world_map[index_x, index_y] = Instantiate(tile_flat, new Vector3(x * GameManager.GetGameManager().TileSize, y * GameManager.GetGameManager().TileSize, y / 10.0f), Quaternion.identity) as GameObject;
							tile_count++;
						}
					}
				}
			}
		}

		// Round edges
		for (int iter = 0; iter < 4; iter++)
		{
			for (int x = 0; x < this.world_width; x++)
			{
				for (int y = 0; y < this.world_height; y++)
				{
					int count = this.CountSurroundingTraversible(x, y);
					if ((count < 5 && count > 1) && this.world_map[x, y] != null)
					{
						Destroy(this.world_map[x, y]);
						this.world_map[x, y] = null;
						tile_count--;
					}
				}
			}
		}

		//do
		//{
		//	this.world_portal_x = this.random_generator.Next(this.world_width);
		//	this.world_protal_y = this.random_generator.Next(this.world_height);
		//} while (this.CountSurroundingTraversible(this.world_portal_x, this.world_protal_y) != 9);

		int x_out = 0;
		int y_out = 0;
		this.TranslateWorldToArray(this.world_portal_x, this.world_protal_y, ref x_out, ref y_out);
		this.portal = Instantiate(def.portal, new Vector3((x_out - this.world_width / 2) * GameManager.GetGameManager().TileSize, (y_out - this.world_height / 2) * GameManager.GetGameManager().TileSize, (y_out - this.world_height / 2) / 10.0f), Quaternion.identity) as GameObject;
		
		for (int iter = 0; iter < 4; iter++)
		{
			for (int x = 0; x < this.world_width; x++)
			{
				for (int y = 0; y < this.world_height; y++)
				{
					int count = this.CountSurroundingTraversible(x, y);
					if (count == 9 && this.random_generator.Next(this.blocking_1_in) == 0 && this.blocking_iter < 1000)
					{
						GameObject tile_blocking = level_assets.GetInstance().GetTileFromSet(tile_set, "blocking");
						if (tile_blocking != null)
						{
							this.world_map[x, y].GetComponent<tile>().SetTraversible(false);
							this.blocking_objects[this.blocking_iter] = Instantiate(tile_blocking, new Vector3((x - this.world_width / 2) * GameManager.GetGameManager().TileSize, (y - this.world_height / 2) * GameManager.GetGameManager().TileSize, (y - this.world_height / 2) / 10.0f), Quaternion.identity) as GameObject;
							this.blocking_iter++;
						}
					}
				}
			}
		}


        // Iterate over rooms in generated list.
        foreach (BetterRoom room in rooms)
		{
			// Iterate over the x indexes of the current room.
			for (int x = room.left; x <= room.right; x++)
			{
				// Iterate over the y indexes of the current room.
				for (int y = room.top; y <= room.bottom; y++)
				{
					// Initiliase the index variables for translation.
					int index_x = 0;
					int index_y = 0;

					// Check that the world space was succesfully translated to an array indexer.
					// True - Set the type of the title on the world map to be a traversible space.
					if (this.TranslateWorldToArray(x, y, ref index_x, ref index_y))
					{
						if (this.world_map[index_x, index_y] != null)
						{
							if (this.IsNull_array(index_x - 1, index_y))
							{
								Destroy(this.world_map[index_x, index_y]);
								this.world_map[index_x, index_y] = Instantiate(tile_left, new Vector3(x * GameManager.GetGameManager().TileSize, y * GameManager.GetGameManager().TileSize, 0.0f), Quaternion.identity) as GameObject;
							}
							else if (this.IsNull_array(index_x + 1, index_y))
							{
								Destroy(this.world_map[index_x, index_y]);
								this.world_map[index_x, index_y] = Instantiate(tile_right, new Vector3(x * GameManager.GetGameManager().TileSize, y * GameManager.GetGameManager().TileSize, 0.0f), Quaternion.identity) as GameObject;
							}
						}
					}
				}
			}

		}


        // TODO: SPAWN ITEMS AND MONSTERS
        for( int iter = 0; iter < 4; iter++ )
        {
            for( int x = 0; x < this.world_width; x++ )
            {
                for( int y = 0; y < this.world_height; y++ )
                {
                    if( IsTraversible_array( x, y ) )
                    {
                        if( this.random_generator.Next( 150 ) == 0 )
                        {
                            // Spawn enemy
                            TheShitISpawned.Add( Instantiate( def.monsters[0], new Vector3( ( x - this.world_width / 2 ) * GameManager.GetGameManager().TileSize, ( y - this.world_height / 2 ) * GameManager.GetGameManager().TileSize, ( y - this.world_height / 2 ) / 10.0f ), Quaternion.identity ) );
                        }
                        else if( this.random_generator.Next( 300 ) == 0 )
                        {
                            // Spawn item
                            TheShitISpawned.Add( Instantiate( def.items[this.random_generator.Next( 3 )], new Vector3( ( x - this.world_width / 2 ) * GameManager.GetGameManager().TileSize, ( y - this.world_height / 2 ) * GameManager.GetGameManager().TileSize, ( y - this.world_height / 2 ) / 10.0f ), Quaternion.identity ) );
                        }
                    }
                }
            }
        }
    }

	int CountSurroundingTraversible(int _x, int _y)
	{
		int count = 0;

		for (int x = _x - 1; x <= _x + 1; x++)
		{
			for (int y = _y - 1; y <= _y + 1; y++)
			{
				if (this.IsTraversible_array(x, y) && x != this.world_portal_x && y != this.world_protal_y)
				{
					count++;
				}
			}
		}

		return (count);
	}

	bool IsOverlapping(int curr, ref BetterRoom[] all)
	{
		for (int i = 0; i < all.Length; i++)
		{
			if (i != curr)
			{
				if (all[curr].IsOverlapping(all[i]))
				{
					return (true);
				}
			}
		}

		return (false);
	}	
}
