
namespace NPerf.Core
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;


    /// <summary>
    /// TODO - Add class summary
    /// </summary>
    /// <remarks>
    /// 	created by - dehalleux
    /// 	created on - 21/01/2004 15:06:22
    /// </remarks>
    [Serializable]
    [XmlRoot("os")]
    public class PerfOs
    {
        private static readonly object Sync = new object();

        private PerfOs()
        {
        }

        /// <summary>
        /// Retreives the current machine operating system information.
        /// </summary>
        /// <returns><see cref="PerfOs"/> object describing the current
        /// operation system</returns>
        public static PerfOs GetCurrent()
        {
            lock (Sync)
            {
                return new PerfOs
                           {
                               Name = Environment.OSVersion.Platform.ToString(),
                               Version = Environment.Version.ToString()
                           };
            }
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }
    }
}
