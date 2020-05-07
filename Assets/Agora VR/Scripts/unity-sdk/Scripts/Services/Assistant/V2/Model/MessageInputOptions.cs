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

using Newtonsoft.Json;

namespace IBM.Watson.Assistant.V2.Model
{
    /// <summary>
    /// Optional properties that control how the assistant responds.
    /// </summary>
    public class MessageInputOptions
    {
        /// <summary>
        /// Whether to return additional diagnostic information. Set to `true` to return additional information under
        /// the `output.debug` key.
        /// </summary>
        [JsonProperty("debug", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Debug { get; set; }
        /// <summary>
        /// Whether to restart dialog processing at the root of the dialog, regardless of any previously visited nodes.
        /// **Note:** This does not affect `turn_count` or any other context variables.
        /// </summary>
        [JsonProperty("restart", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Restart { get; set; }
        /// <summary>
        /// Whether to return more than one intent. Set to `true` to return all matching intents.
        /// </summary>
        [JsonProperty("alternate_intents", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AlternateIntents { get; set; }
        /// <summary>
        /// Whether to return session context with the response. If you specify `true`, the response will include the
        /// `context` property.
        /// </summary>
        [JsonProperty("return_context", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReturnContext { get; set; }
    }
}
