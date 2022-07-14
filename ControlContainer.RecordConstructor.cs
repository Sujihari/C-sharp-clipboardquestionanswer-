﻿using System.ComponentModel;

namespace it
{
    internal sealed partial class ControlContainer
    {
        /// <summary>
        /// Record Constructor
        /// </summary>
        /// <param name="components">
        /// <see cref="Components" />
        /// </param>
        public ControlContainer(ComponentCollection components = default)
        {
            Components = components;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ControlContainer);
        }
    }
}
