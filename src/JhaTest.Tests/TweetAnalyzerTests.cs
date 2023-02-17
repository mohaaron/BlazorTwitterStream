using Jha.Models;
using Jha.Services;
using Moq;
using Xunit.Abstractions;

namespace JhaTest.Tests
{
    public class Tests
    {
        [Fact]
        public void Gets_Top10_Tags_In_Rank_Order()
        {
            ITweetAnalyzer tweetAnalyzer = new TweetAnalyzer();
            
            // Add 20 tweets with 15 hashtags
            foreach (var tweet in Tweets)
            {
                tweetAnalyzer.Add(tweet);
            }
            
            var hashtags = tweetAnalyzer.GetTop10Hashtags();

            Assert.Collection(hashtags, 
                hashtag => Assert.Equal(1, hashtag.Rank),
                hashtag => Assert.Equal(2, hashtag.Rank),
                hashtag => Assert.Equal(3, hashtag.Rank),
                hashtag => Assert.Equal(4, hashtag.Rank),
                hashtag => Assert.Equal(5, hashtag.Rank),
                hashtag => Assert.Equal(6, hashtag.Rank),
                hashtag => Assert.Equal(7, hashtag.Rank),
                hashtag => Assert.Equal(8, hashtag.Rank),
                hashtag => Assert.Equal(9, hashtag.Rank),
                hashtag => Assert.Equal(10, hashtag.Rank)
            );
        }

        [Fact]
        public void Gets_Top10_Tags_In_Count_Order()
        {
            ITweetAnalyzer tweetAnalyzer = new TweetAnalyzer();

            // Add 20 tweets with 15 hashtags
            foreach (var tweet in Tweets)
            {
                tweetAnalyzer.Add(tweet);
            }

            var hashtags = tweetAnalyzer.GetTop10Hashtags();

            Assert.Collection(hashtags,
                hashtag => Assert.Equal(10, hashtag.Count),
                hashtag => Assert.Equal(7, hashtag.Count),
                hashtag => Assert.Equal(5, hashtag.Count),
                hashtag => Assert.Equal(5, hashtag.Count),
                hashtag => Assert.Equal(4, hashtag.Count),
                hashtag => Assert.Equal(3, hashtag.Count),
                hashtag => Assert.Equal(3, hashtag.Count),
                hashtag => Assert.Equal(2, hashtag.Count),
                hashtag => Assert.Equal(2, hashtag.Count),
                hashtag => Assert.Equal(2, hashtag.Count)
            );
        }

        [Fact]
        public void Gets_Top10_Tags_Count()
        {
            ITweetAnalyzer tweetAnalyzer = new TweetAnalyzer();

            // Add 20 tweets with 15 hashtags
            foreach (var tweet in Tweets)
            {
                tweetAnalyzer.Add(tweet);
            }

            var hashtags = tweetAnalyzer.GetTop10Hashtags();

            Assert.Equal(10, hashtags.Count);
        }

        #region Test Data
        public static List<Tweet> Tweets
        {
            get
            {
                return new List<Tweet>
                {
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("bbbbbbb"),
                            new Hashtag("aaaaaaaaaaa"),
                            new Hashtag("dddddddddd"),
                            new Hashtag("dddddddddd"),
                            new Hashtag("gggggggggggg"),
                        }
                    },
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("bbbbbbb"),
                            new Hashtag("aaaaaaaaaaa"),
                            new Hashtag("eeeeeeeeee"),
                            new Hashtag("gggggggggggg"),
                            new Hashtag("gggggggggggg"),
                        }
                    },
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("bbbbbbb"),
                            new Hashtag("aaaaaaaaaaa"),
                            new Hashtag("eeeeeeeeee"),
                            new Hashtag("eeeeeeeeee"),
                            new Hashtag("eeeeeeeeee"),
                        }
                    },
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("bbbbbbb"),
                            new Hashtag("aaaaaaaaaaa"),
                            new Hashtag("jjjjjjjjj"),
                            new Hashtag("jjjjjjjjj"),
                            new Hashtag("oooooooooo"),
                        }
                    },
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("bbbbbbb"),
                            new Hashtag("aaaaaaaaaaa"),
                            new Hashtag("jjjjjjjjj"),
                            new Hashtag("jjjjjjjjj"),
                            new Hashtag("oooooooooo"),
                        }
                    },
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("ppppppp"),
                            new Hashtag("ssssssssss"),
                            new Hashtag("jjjjjjjjj"),
                            new Hashtag("wwwwwwwwwww"),
                            new Hashtag("wwwwwwwwwww"),
                        }
                    },
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("ppppppp"),
                        }
                    },
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("ppppppp"),
                            new Hashtag("ssssssssss"),
                        }
                    },
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("jjjjjjjjj"),
                        }
                    },
                    new Tweet()
                    {
                        AuthorId = "23948324",
                        Hashtags = new List<Hashtag>
                        {
                            new Hashtag("uuuuuuu"),
                            new Hashtag("jjjjjjjjj"),
                        }
                    },
                };
            }
        }
        #endregion
    }
}