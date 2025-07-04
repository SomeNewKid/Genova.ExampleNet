// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Attributes;

namespace Genova.ExampleNet.Models;

/// <summary>
/// This class represents a model in the ExampleNet website.
/// </summary>
 // <notes>
 // 1. The purpose of this class is to have any View in the ExampleNet project use this internal model,
 //    which "ties" the view to this project and disallows other websites using the view.
 // </notes>
[CodeQuality(Unsealed = true, Justification = "Intended as base class for all models.")]
internal class Model
{
}
