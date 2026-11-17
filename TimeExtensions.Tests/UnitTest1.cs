namespace TheElm.Time.Tests {
    public class Comparisons {
        [Test]
        public void DateTimeOffsetTest() {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            
            DateTimeOffset behind = now.Subtract(1, TimeUnit.Hours);
            DateTimeOffset ahead = now.Add(4, TimeUnit.Hours);
            
            Assert.Multiple(() => {
                Assert.That(now - TimeSpan.FromHours(1), Is.EqualTo(behind));
                Assert.That(now + TimeSpan.FromHours(4), Is.EqualTo(ahead));
                
                Assert.That(behind.IsBehind(), Is.True);
                Assert.That(ahead.IsAhead(), Is.True);
                
                Assert.That(ahead.LessThanAhead(TimeSpan.FromDays(2)), Is.True);
                Assert.That(ahead.LessThanAhead(TimeSpan.FromMinutes(30)), Is.False);
                Assert.That(ahead.MoreThanAhead(TimeSpan.FromHours(2)), Is.True);
                Assert.That(ahead.MoreThanAhead(TimeSpan.FromHours(8)), Is.False);
                
                Assert.That(behind.LessThanAgo(TimeSpan.FromDays(2)), Is.True);
                Assert.That(behind.LessThanAgo(TimeSpan.FromMinutes(30)), Is.False);
                Assert.That(behind.MoreThanAgo(TimeSpan.FromMinutes(30)), Is.True);
                Assert.That(behind.MoreThanAgo(TimeSpan.FromDays(2)), Is.False);
                
                for ( int i = 1; i <= 30; i++ ) {
                    Assert.That(ahead.LessThanAhead(TimeSpan.FromDays(i)), Is.True, "Value is not less than {0} days ahead", i);
                    Assert.That(ahead.MoreThanAhead(TimeSpan.FromDays(i)), Is.False, "Value is not more than {0} days ahead", i);
                    Assert.That(ahead.MoreThanAhead(TimeSpan.FromMinutes(i)), Is.True, "Value is not more than {0} minutes ahead", i);
                    Assert.That(ahead.LessThanAhead(TimeSpan.FromMinutes(i)), Is.False, "Value is not less than {0} minutes ahead", i);
                }
            });
        }
        
        [Test]
        public void DateTimeTest() {
            DateTime now = DateTime.UtcNow;
            
            DateTime behind = now.Subtract(1, TimeUnit.Hours);
            DateTime ahead = now.Add(4, TimeUnit.Hours);
            
            Assert.Multiple(() => {
                Assert.That(now - TimeSpan.FromHours(1), Is.EqualTo(behind));
                Assert.That(now + TimeSpan.FromHours(4), Is.EqualTo(ahead));
                
                Assert.That(behind.IsBehind(), Is.True);
                Assert.That(ahead.IsAhead(), Is.True);
                
                Assert.That(ahead.LessThanAhead(TimeSpan.FromDays(2)), Is.True);
                Assert.That(ahead.LessThanAhead(TimeSpan.FromMinutes(30)), Is.False);
                Assert.That(ahead.MoreThanAhead(TimeSpan.FromHours(2)), Is.True);
                Assert.That(ahead.MoreThanAhead(TimeSpan.FromHours(8)), Is.False);
                
                Assert.That(behind.LessThanAgo(TimeSpan.FromDays(2)), Is.True);
                Assert.That(behind.LessThanAgo(TimeSpan.FromMinutes(30)), Is.False);
                Assert.That(behind.MoreThanAgo(TimeSpan.FromMinutes(30)), Is.True);
                Assert.That(behind.MoreThanAgo(TimeSpan.FromDays(2)), Is.False);
                
                for ( int i = 1; i <= 30; i++ ) {
                    Assert.That(ahead.LessThanAhead(TimeSpan.FromDays(i)), Is.True, "Value is not less than {0} days ahead", i);
                    Assert.That(ahead.MoreThanAhead(TimeSpan.FromDays(i)), Is.False, "Value is not more than {0} days ahead", i);
                    Assert.That(ahead.MoreThanAhead(TimeSpan.FromMinutes(i)), Is.True, "Value is not more than {0} minutes ahead", i);
                    Assert.That(ahead.LessThanAhead(TimeSpan.FromMinutes(i)), Is.False, "Value is not less than {0} minutes ahead", i);
                }
            });
        }
    }
}
