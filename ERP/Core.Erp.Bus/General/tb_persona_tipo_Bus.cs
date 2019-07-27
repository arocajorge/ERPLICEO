using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.General
{
    public class tb_persona_tipo_Bus
    {
        tb_persona_tipo_Data data = new tb_persona_tipo_Data();

        public List<tb_persona_tipo_Info> get_list()
        {
            try
            {
                return data.get_list();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
    }
