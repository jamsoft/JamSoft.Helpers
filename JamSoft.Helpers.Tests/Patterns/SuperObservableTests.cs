using JamSoft.Helpers.Patterns.Mvvm;
using Xunit;

namespace JamSoft.Helpers.Tests.Patterns
{
    public class SuperObservableTests
    {
        [Fact]
        public void AddRange_Ctor_Adds_Items()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";

            var sut = new SuperObservableCollection<string>(new[] { stringA, stringB, stringC });

            Assert.Equal(3, sut.Count);
        }

        [Fact]
        public void AddRange_Adds_Items()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";

            var sut = new SuperObservableCollection<string>();

            sut.AddRange(new []{ stringA, stringB, stringC });

            Assert.Equal(3, sut.Count);
        }

        [Fact]
        public void Items_Are_Sorted()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";

            var sut = new SuperObservableCollection<string>();

            sut.AddRange(new[] { stringB, stringA, stringC });

            Assert.Equal(stringB, sut[0]);

            sut.Sort();

            Assert.Equal(stringA, sut[0]);
        }

        [Fact]
        public void Items_Are_Sorted_Events()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";
            var stringD = "ddddd";

            var sut = new SuperObservableCollection<string>();

            int notifications = 0;
            sut.CollectionChanged += (sender, args) =>
            {
                notifications++;
            };

            sut.AddRange(new[] { stringB, stringA, stringC, stringD });

            Assert.Equal(stringB, sut[0]);

            sut.Sort(true);

            Assert.Equal(stringA, sut[0]);
            Assert.Equal(1, notifications);
        }

        [Fact]
        public void AddRange_One_Collection_Changed_Notification()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";

            var sut = new SuperObservableCollection<string>();
            int notifications = 0;
            sut.CollectionChanged += (sender, args) =>
            {
                notifications++;
            };

            sut.AddRange(new[] { stringB, stringA, stringC });

            Assert.Equal(1, notifications);
        }

        [Fact]
        public void AddRange_All_Normal_Collection_Changed_Notifications_Fired()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";

            var sut = new SuperObservableCollection<string>();
            int notifications = 0;
            sut.CollectionChanged += (sender, args) =>
            {
                notifications++;
            };

            sut.AddRange(new[] { stringB, stringA, stringC }, false, false);

            Assert.Equal(3, notifications);
        }

        [Fact]
        public void AddRange_All_Normal_Collection_Changed_Notifications_Fired_And_Add_Complete()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";

            var sut = new SuperObservableCollection<string>();
            int notifications = 0;
            sut.CollectionChanged += (sender, args) =>
            {
                notifications++;
            };

            sut.AddRange(new[] { stringB, stringA, stringC }, false, false);

            Assert.Equal(3, notifications);
        }

        [Fact]
        public void AddRange_All_Normal_Collection_Changed_And_Final_Changed_Notifications_Fired_And_Add_Complete()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";

            var sut = new SuperObservableCollection<string>();
            int notifications = 0;
            sut.CollectionChanged += (sender, args) =>
            {
                notifications++;
            };

            sut.AddRange(new[] { stringB, stringA, stringC }, false);

            Assert.Equal(4, notifications);
        }

        [Fact]
        public void Add_Fires_Collection_Changed_Notifications()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";

            var sut = new SuperObservableCollection<string>();
            int notifications = 0;
            sut.CollectionChanged += (sender, args) =>
            {
                notifications++;
            };

            sut.Add(stringA);
            sut.Add(stringB);
            sut.Add(stringC);

            Assert.Equal(3, notifications);
        }

        [Fact]
        public void Add_Suppresses_Collection_Changed_Notifications()
        {
            var stringA = "aaaaa";
            var stringB = "bbbbb";
            var stringC = "ccccc";

            var sut = new SuperObservableCollection<string>();
            int notifications = 0;
            sut.CollectionChanged += (sender, args) =>
            {
                notifications++;
            };

            sut.Add(stringA, true);
            sut.Add(stringB, true);
            sut.Add(stringC, true);

            Assert.Equal(0, notifications);
        }
    }
}
