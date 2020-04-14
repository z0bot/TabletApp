using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models
{
    public class DeleteResponse
    {
        public int n { get; set; }
        public OperationTime opTime { get; set; }
        public string electionId { get; set; }
        public int ok { get; set; }
        public string operationTime { get; set; }
        public int deletedCount { get; set; }
    }

    public class OperationTime
    {
        public string ts { get; set; }
        public int t { get; set; }
    }
}