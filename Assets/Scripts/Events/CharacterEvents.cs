using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    //charcter damaged
    public static UnityAction<GameObject, int> characterDamaged;
    //character healed
    public static UnityAction<GameObject, int> characterHealed;
}

