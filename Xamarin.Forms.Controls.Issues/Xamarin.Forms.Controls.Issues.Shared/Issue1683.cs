using System;

using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;
using System.Collections.Generic;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
using System.Diagnostics;
#endif

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve (AllMembers = true)]
	[Issue (IssueTracker.Github, 1683, "Auto Capitalization Implementation")]
	public class Issue1683 : ContentPage
	{
		const string kContainerId = "Container";
		public Issue1683()
		{
			var layout = new StackLayout() { ClassId = kContainerId };

			List<InputView> intputViews = new List<InputView>()
			{
				new Entry() { ClassId = "EntryNotSet" },
				new Entry() { AutoCapitalization = AutoCapitalization.Default, ClassId = "EntryDefault" },
				new Entry() { AutoCapitalization = AutoCapitalization.None, ClassId = "EntryNone" },
				new Entry() { AutoCapitalization = AutoCapitalization.Words, ClassId = "EntryWords" },
				new Entry() { AutoCapitalization = AutoCapitalization.Sentences, ClassId = "EntrySentences" },
				new Entry() { AutoCapitalization = AutoCapitalization.Characters, ClassId = "EntryCharacters" },
				new Editor() { ClassId = "EditorNotSet" },
				new Editor() { AutoCapitalization = AutoCapitalization.Default, ClassId = "EditorDefault" },
				new Editor() { AutoCapitalization = AutoCapitalization.None, ClassId = "EditorNone" },
				new Editor() { AutoCapitalization = AutoCapitalization.Words, ClassId = "EditorWords" },
				new Editor() { AutoCapitalization = AutoCapitalization.Sentences, ClassId = "EditorSentences" },
				new Editor() { AutoCapitalization = AutoCapitalization.Characters, ClassId = "EditorCharacters" },
			};


			if(Device.RuntimePlatform == Device.UWP)
			{
				layout.Children.Add(new Label() { Text = "Sentence and Character don't do anything on UWP" });
			}

			foreach(InputView child in intputViews)
			{
				var inputs = new StackLayout()
				{
					Orientation =  StackOrientation.Horizontal
				};

				if(child is Entry)
					(child as Entry).Text = "All the Same.";

				if(child is Editor)
					(child as Editor).Text = "All the Same.";


				child.HorizontalOptions = LayoutOptions.FillAndExpand;
				var theLabel = new Label();

				theLabel.SetBinding(Label.TextProperty, new Binding("ClassId", source: child));
				inputs.Children.Add(theLabel);
				inputs.Children.Add(child);
				layout.Children.Add(inputs);
			}

			Button rotate = new Button() { Text = "Change Capitalization Settings. Ensure they update correctly" };

			// This shifts everyones capitalization by one in order
			// to test that updating the field works as expected
			rotate.Clicked += (_, __) =>
			{
				for (int i = 0; i < intputViews.Count; i++)
				{
					var entry1 = intputViews[i];
					var cap1 = ((int)entry1.AutoCapitalization + 1);

					if(!entry1.IsSet(InputView.AutoCapitalizationProperty))
					{
						entry1.AutoCapitalization = AutoCapitalization.Default;
						entry1.ClassId = $"{entry1.GetType().Name}{entry1.AutoCapitalization}";
					}
					else if(!Enum.IsDefined(typeof(AutoCapitalization), cap1))
					{
						cap1 = 0;
						entry1.ClearValue(InputView.AutoCapitalizationProperty);
						entry1.ClassId = $"{entry1.GetType().Name}NotSet";
					}
					else
					{
						entry1.AutoCapitalization = (AutoCapitalization)cap1;
						entry1.ClassId = $"{entry1.GetType().Name}{entry1.AutoCapitalization}";
					}
				}
			};

			layout.Children.Add(rotate);


			Content = new ScrollView()
			{
				Content = layout
			};
		}
	}
}
