﻿using Enigma.D3.AttributeModel;
using Enigma.D3.MemoryModel.Collections;
using Enigma.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.D3.MemoryModel.Core
{
    public class FastAttribGroup : MemoryObject
    {
        public static int SizeOf => SymbolTable.Current.FastAttribGroup.SizeOf;

        public int ID
            => Read<int>(SymbolTable.Current.FastAttribGroup.ID);

        public int Flags
            => Read<int>(SymbolTable.Current.FastAttribGroup.Flags);

        public Ptr<Map<AttributeValue>> PtrMap
            => Read<Ptr<Map<AttributeValue>>>(SymbolTable.Current.FastAttribGroup.PtrMap);

        public Map<AttributeValue> Map
            => Read<Map<AttributeValue>>(SymbolTable.Current.FastAttribGroup.Map);
    }
}
