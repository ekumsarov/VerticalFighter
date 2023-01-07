using VecrticalFighter;
using UnityEngine;
using VecrticalFighter.Model;

public static class MoveFactory 
{
    public static IMovable GetMoveType(MoveType moveType)
    {
        IMovable temp;
        switch (moveType)
        {
            case MoveType.LeftLinear:
                temp = new MoveBase();
                temp = new TickMove(temp);
                temp = new LeftLinerMove(temp);
                return temp;

            case MoveType.LeftStatic:
                temp = new MoveBase();
                temp = new LeftLinerMove(temp);
                return temp;

            case MoveType.LeftStop:
                temp = new MoveBase();
                temp = new TickMove(temp);
                temp = new LeftStop(temp);
                return temp;

            case MoveType.TickMove:
                temp = new MoveBase();
                temp = new TickMove(temp);
                return temp;

            case MoveType.RightLinear:
                temp = new MoveBase();
                temp = new TickMove(temp);
                temp = new RightLinerMove(temp);
                return temp;

            case MoveType.RightStop:
                temp = new MoveBase();
                temp = new TickMove(temp);
                temp = new RightStop(temp);
                return temp;

            case MoveType.RightStatic:
                temp = new MoveBase();
                temp = new RightLinerMove(temp);
                return temp;

            case MoveType.TopDown:
                temp = new MoveBase();
                temp = new TickMove(temp);
                temp = new TopDownMove(temp);
                return temp;

            case MoveType.DownTop:
                temp = new MoveBase();
                temp = new DownTopMove(temp);
                return temp;

            default:
                break;
        }

        return new MoveBase();
    }

    public static IMovable GetFollowMove(Transformable target)
    {
        IMovable temp = new MoveBase();
       

        return temp;
    }

    public static IMovable GetMoveByAgnel(float angel)
    {
        IMovable temp;
        temp = new MoveBase();
        temp = new MoveByAngel(temp, angel); 

        return temp;
    }

    public static Vector2 GetStartPosition(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.LeftLinear:
                return new Vector2(Config.Width / 2 + 0.2f, Config.Height / 2 + 0.2f);

            case MoveType.LeftStatic:
                return new Vector2(Config.Width / 2 + 0.2f, Random.Range(0, Config.Height / 2)  + 0.2f);

            case MoveType.LeftStop:
                return new Vector2(-Config.Width / 2 - 0.2f, Random.Range(0, Config.Height / 2) + 0.2f);
                
            case MoveType.RightLinear:
                return new Vector2(-Config.Width / 2 + 0.2f, Config.Height / 2 + 0.2f);

            case MoveType.RightStatic:
                return new Vector2(-Config.Width / 2 + 0.2f, Random.Range(0, Config.Height / 2) + 0.2f);

            case MoveType.RightStop:
                return new Vector2(Config.Width / 2 + 0.3f, Random.Range(0, Config.Height / 2) + 0.2f);

            case MoveType.TopDown:
                return new Vector2(Random.Range(Config.LeftBoardX + 0.1f, Config.Width / 2 - 0.1f), Config.Height / 2 + 0.5f); ;

            case MoveType.DownTop:
                return new Vector2(Random.Range(Config.LeftBoardX + 0.1f, Config.Width / 2 - 0.1f), -Config.Height / 2 - 0.5f); ;

            case MoveType.TickMove:
                return new Vector2(Random.Range(Config.LeftBoardX + 0.1f, Config.Width / 2 - 0.1f), Config.Height / 2 + 0.5f);

            default:
                break;
        }

        return new Vector2(0f, Config.Height / 2 + 2f);
    }
}
