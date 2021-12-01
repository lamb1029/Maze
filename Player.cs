using System;
using System.Collections.Generic;
using System.Text;

namespace maze
{
    class Player
    {
        Board _board;
        Random _random = new Random();
        public int PosY { get; private set; }
        public int PosX { get; private set; }

        public void Initialize(int posY, int posX, int destX, int destY, Board board)
        {
            PosY = posY;
            PosX = posX;

            _board = board;
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                //
                int randValue = _random.Next(0, 4);
                switch(randValue)
                {
                    case 0: //상
                        if (PosY - 1 >= 0 && _board.TILE[PosY - 1, PosX] == Board.TileType.Empty)
                            PosY = PosY - 1;
                        break;
                    case 1: //하
                        if (PosY + 1 < _board.SIZE && _board.TILE[PosY + 1, PosX] == Board.TileType.Empty)
                            PosY = PosY + 1;
                        break;
                    case 2: //좌
                        if (PosX - 1 >= 0 && _board.TILE[PosY, PosX - 1] == Board.TileType.Empty)
                            PosX = PosX - 1;
                        break;
                    case 3: //우
                        if (PosX + 1 < _board.SIZE && _board.TILE[PosY, PosX + 1] == Board.TileType.Empty)
                            PosX = PosX + 1;
                        break;
                }
            }
        }
    }
}
