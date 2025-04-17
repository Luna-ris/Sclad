using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sclad
{
    public partial class Кнопка : Component
    {
        public Кнопка()
        {
            InitializeComponent();
        }

        public Кнопка(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
