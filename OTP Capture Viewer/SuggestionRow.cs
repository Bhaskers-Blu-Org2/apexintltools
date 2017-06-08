using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    class SuggestionRow 
    {
        public int ID
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string OriginalText
        {
            get;
            set;
        }

        public string SuggestedText
        {
            get;
            set;
        }

        public int LanguageId
        {
            get;
            set;
        }

        public string Reviewer
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string TriagedBy
        {
            get;
            set;
        }
    }

    enum TriageState
    {
        NotTriaged=0,
        Approved=1,
        Denied=2
    }
}
