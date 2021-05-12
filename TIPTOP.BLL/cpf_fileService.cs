using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIPTOP.AdoDAL;
using TIPTOP.Model;

namespace TIPTOP.BLL
{
   public class cpf_fileService
    {
        cpf_fileDal cpfDal = new cpf_fileDal();
        public List<cpf_file> Getcpf_fileByIdBuDate(string ID, string BU, string RData)
        {

           
            
            return cpfDal.Getcpf_fileByIdBuDate(ID, BU, RData);
        }

        public List<cpf_file> Getcpf_fileBylistId(List<string> id)
        {
            return cpfDal.Getcpf_fileBylistId(id);
        }
    }
}
