using System;
using System.Collections.Generic;

namespace VUZCodeFirst;

public partial class Lecture
{
    public int LecPk { get; set; }

    public int? TchFk { get; set; }

    public int GrpFk { get; set; }

    public int? SbjFk { get; set; }

    public int? RomFk { get; set; }

    public int IdType { get; set; }

    public decimal Week { get; set; }

    public string DayWeek { get; set; }

    public decimal? Lesson { get; set; }

    public virtual Sgroup GrpFkNavigation { get; set; }

    public virtual LectureType IdTypeNavigation { get; set; }

    public virtual Room RomFkNavigation { get; set; }

    public virtual Subject SbjFkNavigation { get; set; }

    public virtual Teacher TchFkNavigation { get; set; }
}
