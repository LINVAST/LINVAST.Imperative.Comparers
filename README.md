# LINVAST.Imperative.Comparers

[![Issues](https://img.shields.io/github/issues/LINVAST/LINVAST.Imperative.Comparers.svg)](https://github.com/LINVAST/LINVAST.Imperative.Comparers/issues)
[![Stable release](https://img.shields.io/github/release/LINVAST/LINVAST.Imperative.Comparers.svg?label=stable)](https://github.com/LINVAST/LINVAST.Imperative.Comparers/releases)
[![Latest release](https://img.shields.io/github/tag-pre/LINVAST/LINVAST.Imperative.Comparers.svg?label=latest)](https://github.com/LINVAST/LINVAST.Imperative.Comparers/releases)
[![NuGet](https://img.shields.io/nuget/vpre/LINVAST.Imperative.Comparers.svg)](https://nuget.org/packages/LINVAST.Imperative.Comparers)

**This is a "PoC" project made to show how easy it is to implement logic on top of LINVAST ASTs and shows all the benefits of LINVAST. The logic is not complete and therefore using it in your own projects is not advised.**

Simple imperative AST comparer library written on top of LINVAST. Made to be extensible and manageable long-term. Available as a NuGet package.

## Used libraries/tools
- [ANTLR4](https://www.antlr.org/)
- [MathNET.Symbolics](https://symbolics.mathdotnet.com/)

## Examples (using CLI tool):
Several examples can be found in the ![Samples](Samples/) directory. One sample will be shown below.

```sh 
$ linvast cmp
ERROR(S):
  A required value not bound to option name is missing.

  -v, --verbose    Set output to verbose messages.

  --help           Display this help screen.

  --version        Display version information.

  value pos. 0     Required. Specification path.

  value pos. 1     Required. Test source path.
```

### Example for swap sources

Sources: ![valid.c](Samples/swap/valid.c) ![wrong.c](Samples/swap/wrong.c)

```sh
$ linvast cmp Samples/swap/valid.c Samples/swap/wrong.c
```

![swap](Samples/swap/valid_c-wrong_c.PNG)


Sources: ![valid.psc](Samples/swap/valid.psc) ![wrong.c](Samples/swap/wrong.c)

```sh
$ linvast cmp Samples/swap/valid.psc Samples/swap/wrong.c
```

![swap](Samples/swap/valid_psc-wrong_c.PNG)


Sources: ![valid.c](Samples/swap/valid.c) ![refactor.c](Samples/swap/refactor.c) (*Note: Overflow is not checked at the moment*)

```sh
$ linvast cmp Samples/swap/valid.c Samples/swap/refactor.c
```

![swap](Samples/swap/valid_c-refactor_c.PNG)


# Extending library with new comparers

Steps:
- Create new comparer class in the `LINVAST.Imperative.Comparers` namespace
- Extend `ASTNodeComparerBase<T>` base class where `T` is the type of `ASTNode` you wish to implement

Comparers created in this way will be automatically picked up by `ASTNodeComparer` class via reflection.

Comparers can (and it is encouraged for them to) use already existing comparers in their logic. Check out some of the already written ![comparers](LINVAST.Imperative.Comparers/Comparers/) as an example.
