﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.MQ
{
    public interface ISettingProvider
    {
        ConfigSetting GetCurrentSetting();
        
    }
}
