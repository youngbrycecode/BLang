﻿using BLang.Utils;

namespace BLang.Error
{
    /// <summary>
    /// All the different types of errors that can occur in the system.
    /// </summary>
    public enum eParseErrorSeries
    {
        /// <summary>
        /// Errors that can occur during tokenization.
        /// </summary>
        [ParseErrorSeries(seriesPrefix: "TK", 
            description: """
            Errors that can occur when transforming the code to tokens.
            this includes errors caused by incorrectly formatted numbers or strings.
            """)]
        Tokenization,
        [ParseErrorSeries(seriesPrefix: "SN",
            description: """
            Errors that can be found in the first pass of the parser.
            Includes things like missing tokens or syntax that does not make sense in the context
            it's given
            """)]
        Syntax
    }

    public enum eParseError
    {
        #region Tokenization errors

        [ParseError("Unexpected character", eParseErrorSeries.Tokenization, 
            """
            A token found in the code could not be matched to a valid token.
            """)]
        UnexpectedCharacter,

        [ParseError("Invalid Real Literal", eParseErrorSeries.Tokenization, 
            """
            Incorrectly formatted floating point number. 
            A floating point number must include a digit before the decimal place, 
            and at least one digit after the decimal place. If an exponent is used, it can be negative
            but the exponent must be an integer and cannot include a decimal point.

            Correct examples: 
            10.0, 0.1 10e2, 0.1e2

            Incorrect examples:
            .1, 2., 0.e1, 0.1e.12
            """)]
        InvalidRealLiteral,

        /// <summary>
        /// A character literal is formatted incorrectly. 
        /// </summary>
        [ParseError("Invalid character literal", eParseErrorSeries.Tokenization, 
            """
            A character literal is formatted incorrectly.
            A character literal must start with a single quote and include only one char.
            It must be terminated with a closing single quote.
            """)]
        TooManyCharactersInCharLiteral,

        [ParseError("Empty character literal", eParseErrorSeries.Tokenization, 
            """
            A character literal is formatted incorrectly. It must have exactly one
            character within the single quotes.
            """)]
        EmptyCharLiteral,

        /// <summary>
        /// When a number is not formatted correctly for the compiler.
        /// </summary>
        [ParseError("Invalid number literal", eParseErrorSeries.Tokenization, 
            """
            An integer number is not formatted correctly.
            An integer can be formatted in several ways. As a raw number it should be written
            as the number verbatim without commas or underscores.

            Numbers of different bases can be specified using the C format.
            Binary:     0b[0-1]+
            Octal:      0o[0-7]+
            Base 10:    [0-9]+
            Hexidecimal [0-9A-Fa-f]+
            """)]
        InvalidNumberLiteral,

        /// <summary>
        /// If there is an unrecognized escape sequence in a char or string.
        /// </summary>
        [ParseError("Unrecognized escape sequence", eParseErrorSeries.Tokenization,
            """
            An invalid escape sequence was found. See escape sequence documentation.
            """)]
        UnrecognizedEscapeSequence,

        /// <summary>
        /// A new line is created in the middle of a string without closing the quotes first.
        /// </summary>
        [ParseError("New line in string literal", eParseErrorSeries.Tokenization, 
            """
            A string was created that ended the line without closing the quote.
            Strings should start with a double quote and include all the text along with escape 
            sequences followed by a closing quote. It should all be on one line.
            """)]
        NewLineInStringLiteral,

        /// <summary>
        /// A new line is specified in the middle of a char literal without ending the single quotes.
        /// </summary>
        [ParseError("New line in char literal", eParseErrorSeries.Tokenization, 
            """
            A char was created that ended the line without closing the quote.
            Chars should start with a single quote and include a single character followed by a closing quote
            before the line ends.
            """)]
        NewLineInCharLiteral,

        #endregion

        #region Syntax errors

        [ParseError("Unexpected Token in File", eParseErrorSeries.Syntax,
            description: """
            Invalid token found at file level. A file must include import statements at the top
            followed by a module block.
            """)]
        UnexpectedTokenAtFileLevel,
        [ParseError("Unexpected Token", eParseErrorSeries.Syntax,
            description: """
            An invalid token was found in the current context.
            """)]
        UnexpectedToken,
        [ParseError("Identifier Expected", eParseErrorSeries.Syntax,
            description: """
            You did not give an identifier. An identifier is the name of a variable or function.
            """)]
        MissingIdentifier,
        [ParseError("Type Expected", eParseErrorSeries.Syntax,
            description: """
            You did not give a type for the variable definition. A colon was added indicating
            the variable has a type, but no valid type was specified.
            """)]
        MissingTypeSpecifier,
        [ParseError("Missing Initializer", eParseErrorSeries.Syntax,
            description: """
            A variable was declared without an explicit type and must be initialized.
            """)]
        MissingInitializer,
        [ParseError("Missing Semicolon", eParseErrorSeries.Syntax,
            description: """
            The compiler found a missing semicolon deliminating the end of a statement. A statement may
            consume more than one line but must end with a semicolon.
            """)]
        MissingSemicolon,
        [ParseError("Missing Syntax Token", eParseErrorSeries.Syntax,
            description: """
            The compiler found a missing syntax token. Examples include closing array indices or missing
            commas in argument lists.
            """)]
        MissingSyntaxToken,
        [ParseError("Missing Expression", eParseErrorSeries.Syntax,
            description: """
            The compiler did not find an expression when it was required.
            """)]
        MissingExpression,
        [ParseError("Expected Function Body", eParseErrorSeries.Syntax,
            description: """
            A function was declared without specifying a body definition.
            """)]
        ExpectedFunctionBody,
        [ParseError("Invalid For Loop Statement", eParseErrorSeries.Syntax,
            description: """
            An invalid statement was found. A for loop must start with a variable creation, or valid expression.
            A semi colon is also valid if you don't want to do anything at the start.
            """)]
        InvalidForLoopStatement,
        [ParseError("No Else on If Expression", eParseErrorSeries.Syntax, 
            description: """
            An if expression must end with an else clause to ensure all possible cases are handled.
            """)]
        NoElseOnIfExpression

        #endregion

        /**
        SEMANTICS ERROR
        [ParseError("Duplicate Access Modifier", eParseErrorSeries.Semantics
            description: """
            An access modifier was specified more than once.
            """)]
        DuplicateAccessModifier,
        */

    }

    /// <summary>
    /// Class to allow the creation of errors based on the token type.
    /// </summary>
    public static class ParseErrorFactory
    {
        public static ParseError CreateError(Enum error, ParserContext context, Enum expectedToken = null)
        {
            return error switch
            {
                #region Lexical

                eParseError.UnexpectedCharacter => new UnexpectedCharacter(context),
                eParseError.InvalidRealLiteral => new InvalidRealLiteral(context),
                eParseError.TooManyCharactersInCharLiteral => new TooManyCharactersInCharLiteral(context),
                eParseError.EmptyCharLiteral => new EmptyCharLiteral(context),
                eParseError.InvalidNumberLiteral => new InvalidNumberLiteral(context),
                eParseError.UnrecognizedEscapeSequence => new UnrecognizedEscapeSequence(context),
                eParseError.NewLineInStringLiteral => new NewLineInLiteral(context, eTokenType.String),
                eParseError.NewLineInCharLiteral => new NewLineInLiteral(context, eTokenType.Char),

                #endregion

                #region syntactic

                eParseError.UnexpectedTokenAtFileLevel => new UnexpectedTokenAtFileLevel(context),
                eParseError.UnexpectedToken => new UnexpectedToken(context),
                eParseError.MissingIdentifier => new MissingIdentifier(context),
                eParseError.MissingTypeSpecifier => new MissingTypeSpecifier(context),
                eParseError.MissingInitializer => new MissingInitializer(context),
                eParseError.MissingSemicolon => new MissingSemicolon(context),
                eParseError.MissingSyntaxToken => new MissingSyntaxToken(context, expectedToken),
                eParseError.MissingExpression => new MissingExpression(context),
                eParseError.ExpectedFunctionBody => new ExpectedFunctionBody(context),
                eParseError.InvalidForLoopStatement => new InvalidForLoopStatement(context),
                eParseError.NoElseOnIfExpression => new NoElseOnIfExpression(context),

                #endregion

                _ => throw new ArgumentException()
            };
        }
    }

    /// <summary>
    /// The possible error levels in the system.
    /// </summary>
    public enum eErrorLevel
    {
        /// <summary>
        /// Warnings do not affect compilation, but they can indicate a problem with the code.
        /// </summary>
        Warning,

        /// <summary>
        /// An error occurs when the parser can no longer form a binary from the input text, 
        /// At the error level, the parser can continue to run and log more errors and warnings.
        /// 1. Undefined identifiers
        /// 2. Unexpected token
        /// </summary>
        Error,

        /// <summary>
        /// A critical error occurs when an error cannot be recovered from.
        /// Examples of this include:
        /// 1. Invalid token
        /// 2. Some syntax errors
        /// </summary>
        CriticalError
    }
}
