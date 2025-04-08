namespace TheElm.Time.Tests {
    public class Comparisons {
        [Test]
        public void Test1() {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            
            DateTimeOffset behind = now.Subtract(1, TimeUnit.Hours);
            DateTimeOffset ahead = now.Add(4, TimeUnit.Hours);
            
            Assert.Multiple(() => {
                Assert.That(now - TimeSpan.FromHours(1), Is.EqualTo(behind));
                Assert.That(now + TimeSpan.FromHours(4), Is.EqualTo(ahead));
                
                Assert.That(behind.IsBehind(), Is.True);
                Assert.That(ahead.IsAhead(), Is.True);
            });
        }
    }
}
