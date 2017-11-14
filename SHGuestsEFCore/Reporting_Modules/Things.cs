using System;
using System.Collections.Generic;
using System.Text;

namespace SHGuestsEFCore.Reporting_Modules
{
    public class Things : IDisposable
    {

        public string Roster { get; set; }
        public string Gender { get; set; }
        public int? VisitNumber { get; set; }
        public long Guests { get; set; }
        public long BedDays { get; set; }
        public long MinDays { get; set; }
        public long MaxDays { get; set; }
        public double AvgDays { get; set; }


        public Things ()
        {

        }
        public Things (bool blank1or2)
        {
            if ( blank1or2 )
            {
                this.Roster = "----------";
                this.Gender = "------";
                this.VisitNumber = 0;
                this.Guests = 0;
                this.BedDays = 0;
                this.MinDays = 0;
                this.MaxDays = 0;
                this.AvgDays = 0.00;
            }
            else
            {
                this.Roster = string.Empty;
                this.Gender = string.Empty;
                this.VisitNumber = 0;
                this.Guests = 0;
                this.BedDays = 0;
                this.MinDays = 0;
                this.MaxDays = 0;
                this.AvgDays = 0.00;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose( bool disposing )
        {
            if ( !disposedValue )
            {
                if ( disposing )
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Things() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose( )
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose( true );
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
