using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeslaBotConsoleApp.Core.Services
{
    public class TeslaBotCommandService : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("pong");
        }

        [Command("dupa")]
        public async Task Dupa(IUser user)
        {
            await ReplyAsync($"Zamknij dupe {user.Username}");
        }

        [Command("pog")]
        public async Task pog()
        {
            await ReplyAsync("<:pog:925109568332255305>");
        }

        [Command("pogx100")]
        public async Task pogx100()
        {
            await ReplyAsync("<:pog:925109568332255305><:pog:925109568332255305><:pog:925109568332255305><:pog:925109568332255305><:pog:925109568332255305>");
        }

        [Command("zamknijdupe")]
        [RequireUserPermission(GuildPermission.KickMembers, ErrorMessage = "You don't have the permission ``kick_member``!")]
        public async Task zamknijdupe(IGuildUser user, string reason)
        {
            if (user == null)
            {
                await ReplyAsync("Komu mam zamknąć dupe?");
                return;
            }
            await user.KickAsync(reason: reason);
        }

        [Command("zamknijmorde")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.MuteMembers, ErrorMessage = "You don't have the permission ``mute_member``!")]
        public async Task zamknijmorde(IGuildUser user)
        {
            if (user == null)
            {
                await ReplyAsync("Komu mam zamknąć dupe?");
                return;
            }
            if(user.Username == "cinos" || user.Username == "b0zke")
            {
                await ReplyAsync("Sam zamknij morde");
                return;
            }
            await user.ModifyAsync(x => x.Mute = true);
        }

        [Command("flashbang")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.DeafenMembers, ErrorMessage = "You don't have the permission ``deafen_members``!")]
        public async Task flashbang(IGuildUser user)
        {
            if (user == null)
            {
                await ReplyAsync("Komo mam oslepic?");
                return;
            }
            if (user.Username == "cinos" || user.Username == "b0zke")
            {
                await ReplyAsync("Pago flash");
                return;
            }
            await user.ModifyAsync(x => x.Deaf = true);
        }
    }
}