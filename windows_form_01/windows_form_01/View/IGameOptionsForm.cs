using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windows_form_01.Model;

namespace windows_form_01.View
{
    public interface IGameOptionsForm
    {
        string Name { get; set; }
        GameDifficulty GameDifficulty { get; set; }
    }
}
