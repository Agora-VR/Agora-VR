/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.TextToSpeech.V1.Model
{
    /// <summary>
    /// Information about an existing custom voice model.
    /// </summary>
    public class VoiceModel
    {
        /// <summary>
        /// The customization ID (GUID) of the custom voice model. The **Create a custom model** method returns only
        /// this field. It does not not return the other fields of this object.
        /// </summary>
        [JsonProperty("customization_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomizationId { get; set; }
        /// <summary>
        /// The name of the custom voice model.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The language identifier of the custom voice model (for example, `en-US`).
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// The GUID of the credentials for the instance of the service that owns the custom voice model.
        /// </summary>
        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }
        /// <summary>
        /// The date and time in Coordinated Universal Time (UTC) at which the custom voice model was created. The value
        /// is provided in full ISO 8601 format (`YYYY-MM-DDThh:mm:ss.sTZD`).
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public string Created { get; set; }
        /// <summary>
        /// The date and time in Coordinated Universal Time (UTC) at which the custom voice model was last modified. The
        /// `created` and `updated` fields are equal when a voice model is first added but has yet to be updated. The
        /// value is provided in full ISO 8601 format (`YYYY-MM-DDThh:mm:ss.sTZD`).
        /// </summary>
        [JsonProperty("last_modified", NullValueHandling = NullValueHandling.Ignore)]
        public string LastModified { get; set; }
        /// <summary>
        /// The description of the custom voice model.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// An array of `Word` objects that lists the words and their translations from the custom voice model. The
        /// words are listed in alphabetical order, with uppercase letters listed before lowercase letters. The array is
        /// empty if the custom model contains no words. This field is returned only by the **Get a voice** method and
        /// only when you specify the customization ID of a custom voice model.
        /// </summary>
        [JsonProperty("words", NullValueHandling = NullValueHandling.Ignore)]
        public List<Word> Words { get; set; }
    }
}
