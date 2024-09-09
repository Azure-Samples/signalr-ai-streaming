# [Azure SignalR AI Streaming with Azure OpenAI]

[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](placeholder)
[![Open in Dev Containers](https://img.shields.io/static/v1?style=for-the-badge&label=Dev%20Containers&message=Open&color=blue&logo=visualstudiocode)](placeholder)

In the current landscape of digital communication, AI-powered chatbots and streaming technology have become increasingly popular. This project aims to combine these two trends into a seamless group chat application by leveraging SignalR for real-time communication and integrating Azure OpenAI. This project demonstrates SignalR group chats and Azure OpenAI integration.

[Features](#features) • [Gettting Started](#getting-started) • [Guidance](#guidance)

![chat sample](./chat.jpg)

## Important Security Notice

This template, the application code and configuration it contains, has been built to showcase Microsoft Azure specific services and tools. We strongly advise our customers not to make this code part of their production environments without implementing or enabling additional security features.  

## Features

This project provides the following features:

* Group streaming with Azure SignalR
* OpenAI chatbot integration
* Chat has full context of history conversation

### Architecture Diagram

![...Add Image]()

## Getting Started

You have a few options for getting started with this template. The quickest way to get started is [GitHub Codespaces](#github-codespaces), since it will setup all the tools for you, but you can also [set it up locally](#local-environment). You can also use a [VS Code dev container](#vs-code-dev-containers)

This template uses `gpt-35-turbo` which may not be available in all Azure regions. Check for [up-to-date region availability](https://learn.microsoft.com/azure/ai-services/openai/concepts/models#standard-deployment-model-availability) and select a region during deployment accordingly

  * We recommend using [East US]

### GitHub Codespaces

You can run this template virtually by using GitHub Codespaces. The button will open a web-based VS Code instance in your browser:

1. Open the template (this may take several minutes)
    [![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](placeholder)
2. Open a terminal window
3. Sign into your Azure account:

    ```shell
     azd auth login --use-device-code
    ```

4. [any other steps needed for your template]
5. Provision the Azure resources and deploy your code:

    ```shell
    azd up
    ```

6. (Add steps to start up the sample app)

### VS Code Dev Containers

A related option is VS Code Dev Containers, which will open the project in your local VS Code using the [Dev Containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers):

1. Start Docker Desktop (install it if not already installed)
2. Open the project:
    [![Open in Dev Containers](https://img.shields.io/static/v1?style=for-the-badge&label=Dev%20Containers&message=Open&color=blue&logo=visualstudiocode)](placeholder)
3. In the VS Code window that opens, once the project files show up (this may take several minutes), open a terminal window.
4. Sign into your Azure account:

    ```shell
     azd auth login
    ```

5. [any other steps needed for your template]
6. Provision the Azure resources and deploy your code:

    ```shell
    azd up
    ```

7. (Add steps to start up the sample app)

8. Configure a CI/CD pipeline:

    ```shell
    azd pipeline config
    ```

### Local Environment

#### Prerequisites

(ideally very short, if any)

* Install [azd](https://aka.ms/install-azd)
  * Windows: `winget install microsoft.azd`
  * Linux: `curl -fsSL https://aka.ms/install-azd.sh | bash`
  * MacOS: `brew tap azure/azd && brew install azd`
* OS
* Library version
* This template uses [MODEL 1] and [MODEL 2] which may not be available in all Azure regions. Check for [up-to-date region availability](https://learn.microsoft.com/azure/ai-services/openai/concepts/models#standard-deployment-model-availability) and select a region during deployment accordingly
  * We recommend using [SUGGESTED REGION]
* ...

#### Installation

(ideally very short)

* list of any prerequisites
* ...

#### Quickstart

(Add steps to get up and running quickly)

1. Bring down the template code:

    ```shell
    azd init --template [name-of-repo]
    ```

    This will perform a git clone

2. Sign into your Azure account:

    ```shell
     azd auth login
    ```

3. [Packages or anything else that needs to be installed]

    ```shell
    npm install ...
    ```

4. ...
5. Provision and deploy the project to Azure:

    ```shell
    azd up
    ```

6. (Add steps to start up the sample app)

7. Configure a CI/CD pipeline:

    ```shell
    azd pipeline config
    ```

#### Local Development

Describe how to run and develop the app locally

## Guidance

### Region Availability

This template uses [MODEL 1] and [MODEL 2] which may not be available in all Azure regions. Check for [up-to-date region availability](https://learn.microsoft.com/azure/ai-services/openai/concepts/models#standard-deployment-model-availability) and select a region during deployment accordingly
  * We recommend using [SUGGESTED REGION]

### Costs

You can estimate the cost of this project's architecture with [Azure's pricing calculator](https://azure.microsoft.com/pricing/calculator/)

* [Azure Product] - [plan type] [link to pricing for product](https://azure.microsoft.com/pricing/)

### Security

> [!NOTE]
> When implementing this template please specify whether the template uses Managed Identity or Key Vault

This template has either [Managed Identity](https://learn.microsoft.com/entra/identity/managed-identities-azure-resources/overview) or Key Vault built in to eliminate the need for developers to manage these credentials. Applications can use managed identities to obtain Microsoft Entra tokens without having to manage any credentials. Additionally, we have added a [GitHub Action tool](https://github.com/microsoft/security-devops-action) that scans the infrastructure-as-code files and generates a report containing any detected issues. To ensure best practices in your repo we recommend anyone creating solutions based on our templates ensure that the [Github secret scanning](https://docs.github.com/code-security/secret-scanning/about-secret-scanning) setting is enabled in your repos.

## Resources

(Any additional resources or related projects)

* Link to supporting information
* Link to similar sample
* [Develop Python apps that use Azure AI services](https://learn.microsoft.com/azure/developer/python/azure-ai-for-python-developers)
* ...