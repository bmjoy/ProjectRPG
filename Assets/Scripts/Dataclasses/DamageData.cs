using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct DamageData
{
    public int Damage { get; private set; }
    public bool IsCrit { get; private set; }

    public DamageData(int damage, bool isCrit)
    {
        Damage = damage;
        IsCrit = isCrit;
    }
}
