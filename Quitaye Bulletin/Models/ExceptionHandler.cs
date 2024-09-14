using PrintAction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class ExceptionHandler
    {
        private readonly LoggerManager _logger;

        public ExceptionHandler()
        {
            _logger = new LoggerManager();
        }

        public void HandleException(Exception exception)
        {
            try
            {
                //throw exception;
            }
            catch (ArgumentException)
            {
                _logger.LogError($"Erreur d'argument: {exception.StackTrace}");
            }
            catch (InvalidOperationException)
            {
                _logger.LogError($"Erreur d'opération non valide: {exception.StackTrace}");
            }
            catch (FileNotFoundException)
            {
                _logger.LogError($"Erreur de fichier introuvable: {exception.StackTrace}");
            }
            catch (NullReferenceException)
            {
                _logger.LogError($"Erreur de référence nulle: {exception.StackTrace}");
            }
            catch (AccessViolationException)
            {
                _logger.LogError($"Erreur de violation d'accès: {exception.StackTrace}");
            }
            catch (Exception)
            {
                _logger.LogError($"Erreur inconnue: {exception.StackTrace}");
            }
            MsgBox msg = new MsgBox();
            msg.show(exception.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Warning);
            msg.ShowDialog();
        }
    }
}