﻿using System;
using System.Net.Http;
using PactNet.Mocks.MockHttpService.Models;

namespace PactNet
{

    public interface IPactVerifier
    {
        IPactVerifier HonoursPactWith(string consumerName);

       
        IPactVerifier PactUri(string uri, PactUriOptions options = null);

        void Verify(string description = null, string providerState = null);
    }

    public interface IPactMessagingVerifier : IPactVerifier
    {
        IPactMessagingVerifier IAmProvider(string providerName);
        IPactMessagingVerifier BroadCast(string messageDescription, string providerState, dynamic exampleMessage);
    }

    public interface IPactHttpVerifier : IPactVerifier
    {
        /// <summary>
        /// Define a set up and/or tear down action for a specific state specified by the consumer.
        /// This is where you should set up test data, so that you can fulfil the contract outlined by a consumer.
        /// </summary>
        /// <param name="providerState">The name of the provider state as defined by the consumer interaction, which lives in the Pact file.</param>
        /// <param name="setUp">A set up action that will be run before the interaction verify, if the provider has specified it in the interaction. If no action is required please use an empty lambda () => {}.</param>
        /// <param name="tearDown">A tear down action that will be run after the interaction verify, if the provider has specified it in the interaction. If no action is required please use an empty lambda () => {}.</param>
        IPactHttpVerifier ProviderState(string providerState, Action setUp = null, Action tearDown = null);
        IPactHttpVerifier ServiceProvider(string providerName, HttpClient httpClient);
        IPactHttpVerifier ServiceProvider(string providerName, Func<ProviderServiceRequest, ProviderServiceResponse> httpRequestSender);
   
        
    }
}