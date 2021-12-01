using System;
using System.Collections.Generic;
using System.Text;

namespace maze
{
    class Pos
    {
        public int Y;
        public int X;
        public Pos(int y, int x) { Y = y; X = x; }
    }

    class Player
    {
        Board _board;
        Random _random = new Random();
        public int PosY { get; private set; }
        public int PosX { get; private set; }

        enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }
        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();

        public void Initialize(int posY, int posX, Board board)
        {
            PosY = posY;
            PosX = posX;
            _board = board;

            //현재 바라보고 있는 방향을 기준으로 좌표 변화를 나타낸다
            int[] frontY = new int[] { -1, 0, 1, 0};
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));
            while (PosY != board.DestY || PosX != board.DestX)
            {
                //현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
                if(_board.TILE[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    //오른쪽 방향으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4;

                    //앞으로 한 보 전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                //현재 바라보는 방향을 기준으로 전진할 수 있는지 확인
                else if(_board.TILE[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    //앞으로 한 보 전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    //왼쪽 방향으로 90도 회전
                    _dir = (_dir + 1 + 4) % 4;
                }
            }
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }
    }
}
