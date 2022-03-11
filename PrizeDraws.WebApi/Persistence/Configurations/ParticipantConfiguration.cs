﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrizeDraws.WebApi.Models;

namespace PrizeDraws.WebApi.Persistence.Configurations;

public class ParticipantConfiguration:IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p=>p.Name).IsRequired();
    }
}