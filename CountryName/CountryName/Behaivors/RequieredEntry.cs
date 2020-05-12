using System;
using System.Linq;
using Xamarin.Forms;

namespace CountryName
{
    public class RequieredEntry : Behavior<Entry>
    {
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid),
                                    typeof(bool),
                                    typeof(RequieredEntry),
                                    false, BindingMode.OneWayToSource);

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = !string.IsNullOrEmpty(args.NewTextValue);

            ((Entry)sender).BackgroundColor = isValid ? Color.White : Color.Red;

            IsValid = isValid;
        }
    }

    public class RequieredEntryLen3 : Behavior<Entry>
    {
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid),
                                    typeof(bool),
                                    typeof(RequieredEntry),
                                    false, BindingMode.OneWayToSource);

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = !string.IsNullOrEmpty(args.NewTextValue) && args.NewTextValue.Length==3;

            ((Entry)sender).BackgroundColor = isValid ? Color.White : Color.Red;

            IsValid = isValid;
        }
    }


    public class RequieredEntryLen2 : Behavior<Entry>
    {
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid),
                                    typeof(bool),
                                    typeof(RequieredEntry),
                                    false, BindingMode.OneWayToSource);

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = !string.IsNullOrEmpty(args.NewTextValue) && args.NewTextValue.Length == 2;

            ((Entry)sender).BackgroundColor = isValid ? Color.White : Color.Red;

            IsValid = isValid;
        }
    }
}
