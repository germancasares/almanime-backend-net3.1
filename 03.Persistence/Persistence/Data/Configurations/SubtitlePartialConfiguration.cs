﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class SubtitlePartialConfiguration : IEntityTypeConfiguration<SubtitlePartial>
    {
        public void Configure(EntityTypeBuilder<SubtitlePartial> builder)
        {
            builder
                .HasKey(c => new { c.UserID, c.SubtitleID });

            builder
                .Property(c => c.RevisionDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");

            builder
                .HasOne(c => c.User)
                .WithMany(c => c.SubtitlePartials)
                .HasForeignKey(c => c.UserID);

            builder
                .HasOne(c => c.Subtitle)
                .WithMany(c => c.SubtitlePartials)
                .HasForeignKey(c => c.SubtitleID);
        }
    }
}
