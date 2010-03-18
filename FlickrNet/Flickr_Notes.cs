using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Add a note to a picture.
        /// </summary>
        /// <param name="photoId">The photo id to add the note to.</param>
        /// <param name="noteX">The X co-ordinate of the upper left corner of the note.</param>
        /// <param name="noteY">The Y co-ordinate of the upper left corner of the note.</param>
        /// <param name="noteWidth">The width of the note.</param>
        /// <param name="noteHeight">The height of the note.</param>
        /// <param name="noteText">The text in the note.</param>
        /// <returns></returns>
        public string PhotosNotesAdd(string photoId, int noteX, int noteY, int noteWidth, int noteHeight, string noteText)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.photos.notes.add");
            parameters.Add("photo_id", photoId);
            parameters.Add("note_x", noteX.ToString());
            parameters.Add("note_y", noteY.ToString());
            parameters.Add("note_w", noteWidth.ToString());
            parameters.Add("note_h", noteHeight.ToString());
            parameters.Add("note_text", noteText);

            UnknownResponse response = GetResponseCache<UnknownResponse>(parameters);

            System.Xml.XPath.XPathNavigator nav = response.GetXPathNavigator().SelectSingleNode("*/@id");
            return nav == null ? null : nav.Value;
        }

        /// <summary>
        /// Edit and update a note.
        /// </summary>
        /// <param name="noteId">The ID of the note to update.</param>
        /// <param name="noteX">The X co-ordinate of the upper left corner of the note.</param>
        /// <param name="noteY">The Y co-ordinate of the upper left corner of the note.</param>
        /// <param name="noteWidth">The width of the note.</param>
        /// <param name="noteHeight">The height of the note.</param>
        /// <param name="noteText">The new text in the note.</param>
        public void PhotosNotesEdit(string noteId, int noteX, int noteY, int noteWidth, int noteHeight, string noteText)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.photos.notes.edit");
            parameters.Add("note_id", noteId);
            parameters.Add("note_x", noteX.ToString());
            parameters.Add("note_y", noteY.ToString());
            parameters.Add("note_w", noteWidth.ToString());
            parameters.Add("note_h", noteHeight.ToString());
            parameters.Add("note_text", noteText);

            GetResponseCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Delete an existing note.
        /// </summary>
        /// <param name="noteId">The ID of the note.</param>
        public void PhotosNotesDelete(string noteId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.photos.notes.delete");
            parameters.Add("note_id", noteId);

            GetResponseCache<NoResponse>(parameters);
        }
    }
}
