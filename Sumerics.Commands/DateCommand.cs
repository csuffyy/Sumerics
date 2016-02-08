﻿namespace Sumerics.Commands
{
    using System;

	sealed class DateCommand : YCommand
	{
		public DateCommand() : 
            base(0, 1)
		{
		}

        public String Invocation()
		{
			return "date()";
		}

        public String Invocation(String arg)
		{
			return "date(" + arg + ")";
		}
	}
}
