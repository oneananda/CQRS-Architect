using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSSourceGenerator
{
    public class CQRSGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var entities = new[] { "Product", "Order", "Customer" };

            foreach (string entity in entities)
            {
                var source = GenerateCodeForEntity(entity);
                context.AddSource($"{entity}CommandsAndQueries.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        }

        private string GenerateCodeForEntity(string entity)
        {
            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("CQRSArchitect.Application.Generated"))
                .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")))
                .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")));

            var classDeclarations = new List<ClassDeclarationSyntax>
        {
            GenerateCommandClass(entity),
            GenerateCommandHandlerClass(entity),
            GenerateQueryClass(entity),
            GenerateQueryHandlerClass(entity)
        };

            namespaceDeclaration = namespaceDeclaration.AddMembers(classDeclarations.ToArray());

            var syntaxTree = SyntaxFactory.SyntaxTree(namespaceDeclaration);
            return syntaxTree.ToString();
        }

        private ClassDeclarationSyntax GenerateCommandClass(string entity)
        {
            return SyntaxFactory.ClassDeclaration($"Create{entity}Command")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("ICommand")))
                .AddMembers(SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("string"), "Name")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                    ));
        }

        private ClassDeclarationSyntax GenerateCommandHandlerClass(string entity)
        {
            return SyntaxFactory.ClassDeclaration($"Create{entity}CommandHandler")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"CommandHandlerBase<Create{entity}Command>")))
                .AddMembers(SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("Task"), "Handle")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword))
                    .AddParameterListParameters(SyntaxFactory.Parameter(SyntaxFactory.Identifier("command")).WithType(SyntaxFactory.ParseTypeName($"Create{entity}Command")))
                    .WithBody(SyntaxFactory.Block(
                        SyntaxFactory.SingletonList<StatementSyntax>(
                            SyntaxFactory.ParseStatement($"// Handle command logic for {entity}\nreturn Task.CompletedTask;")
                        )
                    )));
        }

        private ClassDeclarationSyntax GenerateQueryClass(string entity)
        {
            return SyntaxFactory.ClassDeclaration($"Get{entity}ByIdQuery")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"IQuery<{entity}>")))
                .AddMembers(SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("Guid"), "Id")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                    ));
        }

        private ClassDeclarationSyntax GenerateQueryHandlerClass(string entity)
        {
            return SyntaxFactory.ClassDeclaration($"Get{entity}ByIdQueryHandler")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"QueryHandlerBase<Get{entity}ByIdQuery, {entity}>")))
                .AddMembers(SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("Task<" + entity + ">"), "Handle")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword))
                    .AddParameterListParameters(SyntaxFactory.Parameter(SyntaxFactory.Identifier("query")).WithType(SyntaxFactory.ParseTypeName($"Get{entity}ByIdQuery")))
                    .WithBody(SyntaxFactory.Block(
                        SyntaxFactory.SingletonList<StatementSyntax>(
                            SyntaxFactory.ParseStatement($"// Handle query logic for {entity}\nreturn Task.FromResult(new {entity}());")
                        )
                    )));
        }

        //private string GenerateCodeForEntity(string entity)
        //{
        //    throw new NotImplementedException();
        //}

        //private void GenerateCodeForEntity()
        //{
        //    throw new NotImplementedException();
        //}

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
