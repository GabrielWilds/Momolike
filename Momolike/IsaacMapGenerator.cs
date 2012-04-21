using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGen
{
    public class IsaacMapGenerator : MapGenerator
    {        
        public IsaacMapGenerator()
        {

        }

        public override Room[,] GenerateRooms(int maxRooms)
        {
            Directions[] directions = (Directions[])Enum.GetValues(typeof(Directions));
            List<Room> rooms = new List<Room>();
            rooms.Add(new Room(new Point(0, 0)));

            while (rooms.Count < maxRooms)
            {
                Room targetRoom = rooms[Randomizer.GetRandomNumber(rooms.Count)];
                var newRoomDirection = GetNeighborWithNoAdditionalNeighbors(rooms, targetRoom);

                if (newRoomDirection == null)
                    continue;

                var newRoom = new Room(targetRoom.GetNeighborCoordinates(newRoomDirection.Value));
                newRoom.AddExit(ReverseDirection(newRoomDirection.Value));
                targetRoom.AddExit(newRoomDirection.Value);

                rooms.Add(newRoom);
            }
            Room[,] roomArray = ConvertListToMap(rooms);

            return roomArray;
        }

        protected Directions? GetNeighborWithNoAdditionalNeighbors<T>(IEnumerable<T> rooms, Room targetRoom) where T : Room
        {
            AVAILABLE_DIRECTIONS.Shuffle();

            foreach (var direction in AVAILABLE_DIRECTIONS)
            {
                var neighborCoordinates = targetRoom.GetNeighborCoordinates(direction);

                if (RoomExistsAtLocation(neighborCoordinates, rooms))
                    continue;

                if (!DoesLocationHaveExistingNeighbors(neighborCoordinates, ReverseDirection(direction), rooms))
                    return direction;
            }

            return null;
        }

        protected bool DoesLocationHaveExistingNeighbors<T>(Point locationToCheck, Directions originDirection, IEnumerable<T> rooms) where T : Room
        {
            foreach(var direction in AVAILABLE_DIRECTIONS)
            {
                if(originDirection == direction)
                    continue;
                
                if(RoomExistsAtLocation(Room.GetNeighborCoordinates(locationToCheck, direction), rooms))
                    return true;
            }
            
            return false;
        }
    }
}
