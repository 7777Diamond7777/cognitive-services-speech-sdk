//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

using System;
using System.Globalization;
using static Microsoft.CognitiveServices.Speech.Internal.SpxExceptionThrower;

namespace Microsoft.CognitiveServices.Speech.Translation
{
    /// <summary>
    /// Define payload of translation recognizing/recognized events.
    /// </summary>
    public class TranslationRecognitionEventArgs : RecognitionEventArgs
    {
        internal TranslationRecognitionEventArgs(IntPtr eventHandlePtr)
            : base(eventHandlePtr)
        {
            IntPtr result = IntPtr.Zero;
            ThrowIfFail(Internal.Recognizer.recognizer_recognition_event_get_result(eventHandle, out result));
            Result = new TranslationRecognitionResult(result);
        }

        /// <summary>
        /// Specifies the recognition result.
        /// </summary>
        public TranslationRecognitionResult Result { get; }

        /// <summary>
        /// Returns a string that represents the speech recognition result event.
        /// </summary>
        /// <returns>A string that represents the speech recognition result event.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,"SessionId:{0} Result:{1}.", SessionId, Result.ToString());
        }

    }

    /// <summary>
    /// Define payload of translation text result recognition canceled result events.
    /// </summary>
    public sealed class TranslationRecognitionCanceledEventArgs : TranslationRecognitionEventArgs
    {
        internal TranslationRecognitionCanceledEventArgs(IntPtr eventHandlePtr)
            : base(eventHandlePtr)
        {
            var cancellation = CancellationDetails.FromResult(Result);
            Reason = cancellation.Reason;
            ErrorCode = cancellation.ErrorCode;
            ErrorDetails = cancellation.ErrorDetails;
        }

        /// <summary>
        /// The reason the recognition was canceled.
        /// </summary>
        public CancellationReason Reason { get; }

        /// <summary>
        /// The error code in case of an unsuccessful recognition (<see cref="Reason"/> is set to Error).
        /// If Reason is not Error, ErrorCode returns NoError.
        /// Added in version 1.1.0.
        /// </summary>
        public CancellationErrorCode ErrorCode { get; }

        /// <summary>
        /// The error message in case of an unsuccessful recognition (<see cref="Reason"/> is set to Error).
        /// </summary>
        public string ErrorDetails { get; }

        /// <summary>
        /// Returns a string that represents the speech recognition result event.
        /// </summary>
        /// <returns>A string that represents the speech recognition result event.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,"SessionId:{0} ResultId:{1} CancellationReason:{2}. CancellationErrorCode:{3}", SessionId, Result.ResultId, Reason, ErrorCode);
        }

    }
}