using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task EnviarMensaje(string receptorId, string mensaje)
        {
            var remitenteId = Context.UserIdentifier;

            if (string.IsNullOrEmpty(receptorId) || string.IsNullOrEmpty(mensaje))
            {
                throw new ArgumentException("El receptor y el mensaje no pueden estar vacíos.");
            }

            try
            {
                await Clients.User(receptorId).SendAsync("RecibirMensaje", remitenteId, mensaje);
            }
            catch (Exception ex)
            {
                // Manejar el error (log, notificación, etc.)
                throw new Exception("Error al enviar el mensaje.", ex);
            }
        }

        public async Task UnirseGrupo(string grupo)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, grupo);
        }

        public async Task SalirGrupo(string grupo)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, grupo);
        }

        public async Task EnviarMensajeAGrupo(string grupo, string mensaje)
        {
            var remitenteId = Context.UserIdentifier;

            if (string.IsNullOrEmpty(grupo) || string.IsNullOrEmpty(mensaje))
            {
                throw new ArgumentException("El grupo y el mensaje no pueden estar vacíos.");
            }

            await Clients.Group(grupo).SendAsync("RecibirMensaje", remitenteId, mensaje);
        }

        public override async Task OnConnectedAsync()
        {
            // Lógica al conectar un usuario
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Lógica al desconectar un usuario
            await base.OnDisconnectedAsync(exception);
        }
    }
}
