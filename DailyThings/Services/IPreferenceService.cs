using System;
using System.Collections.Generic;
using System.Text;

namespace DailyThings.Services {
    public interface IPreferenceService {
        /// <param name="key">Preference key.</param>
        /// <param name="value">Preference value.</param>
        /// <summary>Sets a value for a given key.</summary>
        void Set(string key, string value);

        /// <param name="key">Preference key.</param>
        /// <param name="defaultValue">Default value to return if the key does not exist.</param>
        /// <summary>Gets the value for a given key, or the default specified if the key does not exist.</summary>
        void Get(string key, string defaultValue);
    }
}
