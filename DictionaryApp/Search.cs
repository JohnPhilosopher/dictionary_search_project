﻿using System;
using System.Text.RegularExpressions;

namespace DictionaryApp
{
    public class Search : SearchInterface
    {
        // Search term and the content to be searched,
        // as well as search properties for regex
        private string searchTerm;
        private string content;
        private string searchProperties = @"(?im)^";
        // Values to use for mainPattern and relatedPattern
        private string MainPattern
        {
            get => this.searchProperties + this.searchTerm + ".*";
        }
        private string RelatedPattern
        {
            get => this.searchProperties + ".+" + this.searchTerm + ".*";
        }
        // Checks whether search term was valid or not
        private bool isvalid;
        // Match collections of the main and related matches
        // obtained from the content
        private MatchCollection mainMatches;
        private MatchCollection relatedMatches;
        // method: setContent
        // content (string) - The content to set for the search object
        // Sets the content that is being searched for the Search instance.
        public void setContent(string content)
        {
            this.content = content;
        }
        // method: search
        // searchTerm (string) - The search term to look for in content
        // Obtains both the main and related matches of type MatchCollection if the
        // search function has been called successfully with a valid search term.
        // The items of mainMatches and relatedMatches will be in lexicographical order.
        // If there is an issue with the search term, the isvalid value will 
        // be set to false. Otherwise, it will be set to true. An Exception
        // object will be returned if there is an issue. Otherwise, null will
        // be returned.
        public Exception search(string searchTerm)
        {
            this.searchTerm = searchTerm;
            // Check if search term is not null and length is longer than 0
            if (searchTerm != null && searchTerm.Length > 0)
            {
                // Check for unexpected errors
                try
                {
                    this.mainMatches = Regex.Matches(this.content, this.MainPattern);
                    this.relatedMatches = Regex.Matches(this.content, this.RelatedPattern);
                    if (this.mainMatches != null && this.relatedMatches != null)
                    {
                        this.isvalid = true;
                        return null;
                    }
                    this.isvalid = false;
                    return new Exception("Unexpected error occurred");
                }
                // If there are unexpected errors, return exception and set isvalid to false
                catch (Exception e)
                {
                    this.mainMatches = null;
                    this.relatedMatches = null;
                    this.isvalid = false;
                    return e;
                }
            }
            // If the search term is null and/or has a length of 0, return false
            // and exception
            else
            {
                this.isvalid = false;
                return new Exception("There were no search terms entered.");
            }
        }
        // method: getMainMatches
        // Returns the main matches as MatchCollection if successful.
        // Otherwise, it returns null if unsuccessful or no search conducted.
        public MatchCollection getMainMatches()
        {
            return this.mainMatches;
        }
        // method: getRelatedMatches
        // Returns the related matches as MatchCollection if successful.
        // Otherwise, it returns null if unsuccessful or no search conducted.
        public MatchCollection getRelatedMatches()
        {
            return this.relatedMatches;
        }
        // method: isValid
        // Returns if the search term used was valid
        public bool isValid()
        {
            return this.isvalid;
        }
    }
}
