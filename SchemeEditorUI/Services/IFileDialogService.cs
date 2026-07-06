using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeEditorUI.Services
{
    public interface IFileDialogService
    {
        string? SaveFile();
        string? LoadFile();
    }
}
