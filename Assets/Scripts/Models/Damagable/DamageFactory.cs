using VecrticalFighter;

public static class DamageFactory
{
    public static IDamagableType GetMDamageType(DamageType damageType)
    {
        IDamagableType temp;
        switch (damageType)
        {
            case DamageType.Simple:
                temp = new SimpleDamage();
                return temp;

            case DamageType.Shield:
                temp = new ShieldDamage();
                return temp;

            case DamageType.Immortal:
                temp = new ImmortalDamage();
                return temp;

            default:
                break;
        }

        return new SimpleDamage();
    }
}