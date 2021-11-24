using System;
using UnityEngine;

namespace Gameplay.Managers
{
    public static class LayerIndication
    {
        private const String PlateLayer   = "Plate";
        private const String RecycleLayer = "Recycle";
        
        public static readonly int Plate   = LayerMask.GetMask(PlateLayer);
        public static readonly int Recycle = LayerMask.GetMask(RecycleLayer);

    }
}
