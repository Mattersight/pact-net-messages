using PactNetMessages.Models;


namespace PactNetMessages.Validators
{
    internal interface IPactValidator<in TPactFile> where TPactFile : PactFile
    {
        void Validate(TPactFile pactFile, ProviderStates providerStates);
    }
}