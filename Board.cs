using System;
using System.Collections.Generic;
using System.Text;

namespace maze
{
    class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] TILE { get; private set; }
        public int SIZE { get; private set; }
        public int DestY { get; private set; }
        public int DestX { get; private set; }
        Player _player;

        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0)
                return;

            _player = player;

            TILE = new TileType[size, size];
            SIZE = size;
            DestY = size - 2;
            DestX = size - 2;

            //mazes for Programmers
            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }

        private void GenerateByBinaryTree()
        {
            //Binary Tree Algorithm
            //길 막는 작업
            for (int y = 0; y < SIZE; y++)
            {
                for (int x = 0; x < SIZE; x++)
                {
                    //if (x == 0 || x == SIZE - 1 || y == 0 || y == SIZE - 1)
                    if (x % 2 == 0 || y % 2 == 0)
                        TILE[y, x] = TileType.Wall;
                    else
                        TILE[y, x] = TileType.Empty;
                }
            }
            //랜덤으로 우측이나 아래로 길 뚫기
            Random rand = new Random();
            for (int y = 0; y < SIZE; y++)
            {
                for (int x = 0; x < SIZE; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;
                    if (y == SIZE - 2 && x == SIZE - 2)
                        continue;
                    if (y == SIZE - 2)
                    {
                        TILE[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == SIZE - 2)
                    {
                        TILE[y + 1, x] = TileType.Empty;
                        continue;
                    }
                    if (rand.Next(0, 2) == 0)
                    {
                        TILE[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        TILE[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        private void GenerateBySideWinder()
        {
            //길 막는 작업
            for (int y = 0; y < SIZE; y++)
            {
                for (int x = 0; x < SIZE; x++)
                {
                    //if (x == 0 || x == SIZE - 1 || y == 0 || y == SIZE - 1)
                    if (x % 2 == 0 || y % 2 == 0)
                        TILE[y, x] = TileType.Wall;
                    else
                        TILE[y, x] = TileType.Empty;
                }
            }
            Random rand = new Random();
            for (int y = 0; y < SIZE; y++)
            {
                int count = 1;
                for (int x = 0; x < SIZE; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;
                    if (y == SIZE - 2 && x == SIZE - 2)
                        continue;
                    if (y == SIZE - 2)
                    {
                        TILE[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == SIZE - 2)
                    {
                        TILE[y + 1, x] = TileType.Empty;
                        continue;
                    }
                    if (rand.Next(0, 2) == 0)
                    {
                        TILE[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        TILE[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < SIZE; y++)
            {
                for (int x = 0; x < SIZE; x++)
                {
                    //플레이어 좌표를 가져와서 그좌표 생상을 표시
                    if( y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if( y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = GetTileColor(TILE[y, x]);

                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch(type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
