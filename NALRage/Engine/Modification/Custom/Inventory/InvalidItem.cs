// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

// FOR EDITORS - THIS FILE IS NOT COMPILED ANYMORE

namespace NALRage.Engine.Modification.Custom.Inventory
{
    internal class InvalidItem : Item
    {
        private string v;
        private int count;

        public InvalidItem(string v, int count)
        {
            this.v = v;
            this.count = count;
        }

        public override string Name => "Invalid";

        public override UseResult Use()
        {
            // This method meant to be nothing.

            return UseResult.Normal;
        }
    }
}