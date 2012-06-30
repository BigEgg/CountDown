using System;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using BigEgg.Framework.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigEgg.Framework.Test.UnitTesting
{
    [TestClass]
    public class CanExecuteChangedEventTest
    {
        [TestMethod]
        public void CommandCanExecuteChangedTest()
        {
            DelegateCommand command = new DelegateCommand(() => { });

            AssertHelper.CanExecuteChangedEvent(command, () => command.RaiseCanExecuteChanged());


            AssertHelper.ExpectedException<ArgumentNullException>(
                () => AssertHelper.CanExecuteChangedEvent(null, () => command.RaiseCanExecuteChanged()));
            AssertHelper.ExpectedException<ArgumentNullException>(
                () => AssertHelper.CanExecuteChangedEvent(command, null));


            AssertHelper.ExpectedException<AssertException>(() =>
                AssertHelper.CanExecuteChangedEvent(command, () => { }));

            AssertHelper.ExpectedException<AssertException>(() =>
                AssertHelper.CanExecuteChangedEvent(command, () =>
                {
                    command.RaiseCanExecuteChanged();
                    command.RaiseCanExecuteChanged();
                }));
        }

        [TestMethod]
        public void WrongEventSenderTest()
        {
            WrongCommand command = new WrongCommand();

            AssertHelper.ExpectedException<AssertException>(() =>
                AssertHelper.CanExecuteChangedEvent(command, () => command.RaiseCanExecuteChanged()));
        }


        private class WrongCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;


            public bool CanExecute(object parameter)
            {
                throw new NotImplementedException();
            }

            public void Execute(object parameter)
            {
                throw new NotImplementedException();
            }

            public void RaiseCanExecuteChanged()
            {
                if (CanExecuteChanged != null) { CanExecuteChanged(null, EventArgs.Empty); }
            }
        }
    }
}
