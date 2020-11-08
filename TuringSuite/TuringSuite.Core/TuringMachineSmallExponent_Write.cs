using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringSuite.Core.Error;

namespace TuringSuite.Core
{
    public partial class TuringMachineSmallExponent : ITuringMachine<int, byte>
    {
        //private void WriteSymbol(byte symbol)
        //{
        //    // If there's no change, we're done.
        //    if (_currentNode.Value.Symbol == symbol)
        //    {
        //        return;
        //    }

        //    LinkedListNode<SmallExponentTapeCell> oldCurrentNode = _currentNode;
        //    LinkedListNode<SmallExponentTapeCell> nodeCellSmall;
        //    LinkedListNode<SmallExponentTapeCell> nodeCellRemainder;

        //    // Middle change. Doesn't depend on any other nodes.
        //    if (_headExponent > 1 && _headExponent < oldCurrentNode.Value.Exponent)
        //    {
        //        // Keep the left cell. Create a new single cell for this write value,
        //        // and a new cell to the right of that for the remaining exponent.
        //        var originalExponent = oldCurrentNode.Value.Exponent;
        //        var rightExponent = originalExponent - _headExponent;
        //        var leftExponent = _headExponent - 1;
        //        var newCellSmall = new SmallExponentTapeCell(symbol, 1);
        //        var newCellRemainder = new SmallExponentTapeCell(oldCurrentNode.Value.Symbol, rightExponent);

        //        // shrink left cell now that we've split it
        //        oldCurrentNode.Value.Exponent = leftExponent;

        //        nodeCellSmall = _tape.AddAfter(_currentNode, newCellSmall);
        //        nodeCellRemainder = _tape.AddAfter(nodeCellSmall, newCellRemainder);

        //        _currentNode = nodeCellSmall;
                
        //        // We're in the new cell, which only has one value, so set offset to the only value.
        //        _headExponent = 1;

        //        return;
        //    }
        //    // Change the first value. If there is no previous node, or if the previous node
        //    // uses a different symbol then a new node needs to inserted.
        //    else if (_headExponent == 1
        //        && (oldCurrentNode.Previous == null || oldCurrentNode.Previous.Value.Symbol != symbol))
        //    {
        //        // There's enough exponent that merging isn't an issue.
        //        if (oldCurrentNode.Value.Exponent > 1)
        //        {
        //            // Create a new single cell for this write value.
        //            // Insert it before the current cell.
        //            var originalExponent = oldCurrentNode.Value.Exponent;
        //            var newCellSmall = new SmallExponentTapeCell(symbol, 1);

        //            nodeCellSmall = _tape.AddBefore(_currentNode, newCellSmall);

        //            // Now update the oldCurrent exponent
        //            oldCurrentNode.Value.Exponent = originalExponent - 1;

        //            _currentNode = nodeCellSmall;

        //            // We're in the new cell, which only has one value, so set offset to the only value.
        //            _headExponent = 1;

        //            return;
        //        }
        //        // Removing the last value, check to see if the following node
        //        // needs to be combined.
        //        else // oldCurrentNode.Value.Exponent == 1
        //        {
        //            // Following has same symbol, so merge
        //            if (oldCurrentNode.Next != null && oldCurrentNode.Next.Value.Symbol == symbol)
        //            {

        //            }
        //            // Else, next node is null or a different symbol, so just insert this
        //            // single cell.
        //            else
        //            {
        //                // Since this cell already has exponent==1, just reuse this cell.
        //                oldCurrentNode.Value.Symbol = symbol;
        //                oldCurrentNode.Value.Exponent = 1;
        //                _currentNode = oldCurrentNode;
        //                _headExponent = 1;
        //            }
        //        }
        //    }
        //    // Change the first value. If the previous node uses the same symbol then
        //    // increment the exponent.
        //    else if (_headExponent == 1
        //        && oldCurrentNode.Previous != null
        //        && oldCurrentNode.Previous.Value.Symbol == symbol)
        //    {
        //        // No need to create new nodes here, just need to update exponents.
        //        var originalExponent = oldCurrentNode.Value.Exponent;
        //        oldCurrentNode.Value.Exponent = originalExponent - 1;
        //        oldCurrentNode.Previous.Value.Exponent = oldCurrentNode.Previous.Value.Exponent + 1;

        //        // Want to maintain the same head position, which means moving to the previous
        //        // node at the last value.
        //        _currentNode = oldCurrentNode.Previous;
        //        _headExponent = _currentNode.Value.Exponent;

        //        return;
        //    }
        //    // Change the last value. If there is no next node, or if the next node
        //    // uses a different symbol then a new node needs to inserted.
        //    else if (_headExponent == oldCurrentNode.Value.Exponent
        //        && (oldCurrentNode.Next == null || oldCurrentNode.Next.Value.Symbol != symbol))
        //    {
        //        // Create a new single cell for this write value.
        //        // Insert it after the current cell.
        //        var originalExponent = oldCurrentNode.Value.Exponent;
        //        var newCellSmall = new SmallExponentTapeCell(symbol, 1);

        //        nodeCellSmall = _tape.AddAfter(_currentNode, newCellSmall);

        //        // Now update the oldCurrent exponent
        //        oldCurrentNode.Value.Exponent = originalExponent - 1;

        //        _currentNode = nodeCellSmall;

        //        // We're in the new cell, which only has one value, so set offset to the only value.
        //        _headExponent = 1;

        //        return;
        //    }
        //    // Change the last value. If the next node uses the same symbol then
        //    // increment the exponent.
        //    else if (_headExponent == 1
        //        && oldCurrentNode.Next != null
        //        && oldCurrentNode.Next.Value.Symbol == symbol)
        //    {
        //        // No need to create new nodes here, just need to update exponents.
        //        var originalExponent = oldCurrentNode.Value.Exponent;
        //        oldCurrentNode.Value.Exponent = originalExponent - 1;
        //        oldCurrentNode.Next.Value.Exponent = oldCurrentNode.Next.Value.Exponent + 1;

        //        // Want to maintain the same head position, which means moving to the next
        //        // node at the first value.
        //        _currentNode = oldCurrentNode.Next;
        //        _headExponent = 1;

        //        return;
        //    }

        //    throw new InvalidOperationException();
        //}

        private void WriteSymbol(byte symbol)
        {
            // If there's no change, we're done.
            if (_currentNode.Value.Symbol == symbol)
            {
                return;
            }

            if (!AlphabetSymbols.Contains(symbol))
            {
                throw new InvalidOperationException();
            }

            // First check the "no neighbors" branch.
            // Can also get here if the existing neighbor(s) can be
            // ignored because they have a different symbol.
            if (
                (_currentNode.Previous == null
                && _currentNode.Next == null)
                ||
                (_currentNode.Previous != null
                && _currentNode.Next == null
                && _currentNode.Previous.Value.Symbol != symbol)
                ||
                (_currentNode.Previous == null
                && _currentNode.Next != null
                && _currentNode.Next.Value.Symbol != symbol)
                ||
                (_currentNode.Previous != null
                && _currentNode.Next != null
                && _currentNode.Previous.Value.Symbol != symbol
                && _currentNode.Next.Value.Symbol != symbol)
                )
            {
                if (_currentNode.Value.Exponent == 1)
                {
                    Write_One_Overwrite(symbol);
                    return;
                }

                if (_headExponent == 1)
                {
                    Write_First_Solo(symbol);
                    return;
                }
                else if (_headExponent > 1 && _headExponent < _currentNode.Value.Exponent)
                {
                    Write_Mid_Solo(symbol);
                    return;
                }
                else if (_headExponent == _currentNode.Value.Exponent)
                {
                    Write_Last_Solo(symbol);
                    return;
                }
            }
            // Else, Prev node branch
            else if (
                (_currentNode.Previous != null
                && _currentNode.Next == null
                && _currentNode.Previous.Value.Symbol == symbol)
                ||
                (_currentNode.Previous != null
                && _currentNode.Next != null
                && _currentNode.Previous.Value.Symbol == symbol
                && _currentNode.Next.Value.Symbol != symbol)
                )
            {
                if (_currentNode.Value.Exponent == 1)
                {
                    Write_One_MergePrev(symbol);
                    return;
                }

                if (_headExponent == 1)
                {
                    Write_IntoFirst(symbol);
                    return;
                }

                else if (_headExponent > 1 && _headExponent < _currentNode.Value.Exponent)
                {
                    Write_Mid_Solo(symbol);
                    return;
                }
                else if (_headExponent == _currentNode.Value.Exponent)
                {
                    Write_Last_Solo(symbol);
                    return;
                }
            }
            // Else, Next node branch
            else if (
                (_currentNode.Previous == null
                && _currentNode.Next != null
                && _currentNode.Next.Value.Symbol == symbol)
                ||
                (_currentNode.Previous != null
                && _currentNode.Next != null
                && _currentNode.Previous.Value.Symbol != symbol
                && _currentNode.Next.Value.Symbol == symbol)
                )
            {
                if (_currentNode.Value.Exponent == 1)
                {
                    Write_One_MergeNext(symbol);
                    return;
                }

                if (_headExponent == 1)
                {
                    Write_First_Solo(symbol);
                    return;
                }
                else if (_headExponent > 1 && _headExponent < _currentNode.Value.Exponent)
                {
                    Write_Mid_Solo(symbol);
                    return;
                }
                else if (_headExponent == _currentNode.Value.Exponent)
                {
                    Write_IntoLast(symbol);
                    return;
                }
            }
            // Else, both node branch
            else if (
                (_currentNode.Previous != null
                && _currentNode.Next != null
                && _currentNode.Previous.Value.Symbol == symbol
                && _currentNode.Next.Value.Symbol == symbol)
                )
            {
                if (_currentNode.Value.Exponent == 1)
                {
                    Write_One_MergeBoth(symbol);
                    return;
                }

                if (_headExponent == 1)
                {
                    Write_IntoFirst(symbol);
                    return;
                }
                else if (_headExponent > 1 && _headExponent < _currentNode.Value.Exponent)
                {
                    Write_Mid_Solo(symbol);
                    return;
                }
                else if (_headExponent == _currentNode.Value.Exponent)
                {
                    Write_IntoLast(symbol);
                    return;
                }
            }

            // One of the above if statements failed to capture
            // a case that it should have, so something went wrong.
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Changes the symbol on the current node. Should
        /// only get here if the current node Exponent == 1.
        /// No nodes are added or removed.
        /// </summary>
        /// <param name="symbol"></param>
        private void Write_One_Overwrite(byte symbol)
        {
            _headExponent = 1; // no change
            _currentNode.Value.Symbol = symbol;
        }

        /// <summary>
        /// "Appends" a value onto the previous node, and makes the end the head position.
        /// Removes the current node.
        /// This should only be called when there is no Next node.
        /// </summary>
        /// <param name="symbol"></param>
        private void Write_One_MergePrev(byte symbol)
        {
            _currentNode.Previous.Value.Exponent = _currentNode.Previous.Value.Exponent + 1;
            _headExponent = _currentNode.Previous.Value.Exponent;
            _currentNode = _currentNode.Previous;
            _tape.Remove(_currentNode.Next);
        }

        /// <summary>
        /// "Appends" a value onto the next node, and makes the start the head position.
        /// Removes the current node.
        /// This should only be called when there is no Previous node.
        /// </summary>
        /// <param name="symbol"></param>
        private void Write_One_MergeNext(byte symbol)
        {
            _currentNode.Next.Value.Exponent = _currentNode.Next.Value.Exponent + 1;
            _headExponent = 1;
            _currentNode = _currentNode.Next;
            _tape.Remove(_currentNode.Previous);
        }

        /// <summary>
        /// This is called when writing a symbol between two nodes with the same symbol.
        /// Keeps the first, removes the later two nodes.
        /// Example: Write 'A' to *: (A,1)(*,1)(A,1) -> (A,3)
        /// </summary>
        /// <param name="symbol"></param>
        private void Write_One_MergeBoth(byte symbol)
        {
            _headExponent = _currentNode.Previous.Value.Exponent + 1;
            _currentNode.Previous.Value.Exponent =
                _currentNode.Previous.Value.Exponent
                + 1
                + _currentNode.Next.Value.Exponent;
            _currentNode = _currentNode.Previous;
            _tape.Remove(_currentNode.Next.Next);
            _tape.Remove(_currentNode.Next);
        }

        /// <summary>
        /// This is called to write the first symbol in a node.
        /// The node has Exponent > 1.
        /// The adjacent nodes, if they exist, have different symbols.
        /// </summary>
        /// <param name="symbol"></param>
        private void Write_First_Solo(byte symbol)
        {
            _currentNode.Value.Exponent = _currentNode.Value.Exponent - 1;
            _headExponent = 1;

            var newCell = new SmallExponentTapeCell(symbol, 1);
            var newNode = _tape.AddBefore(_currentNode, newCell);
            _currentNode = newNode;
        }

        /// <summary>
        /// This is called to write a symbol in the middle of a node.
        /// The node has Exponent > 1.
        /// </summary>
        /// <param name="symbol"></param>
        private void Write_Mid_Solo(byte symbol)
        {
            // Keep the left cell. Create a new single cell for this write value,
            // and a new cell to the right of that for the remaining exponent.
            var originalExponent = _currentNode.Value.Exponent;
            var rightExponent = originalExponent - _headExponent;
            var leftExponent = _headExponent - 1;
            var newCellSmall = new SmallExponentTapeCell(symbol, 1);
            var newCellRemainder = new SmallExponentTapeCell(_currentNode.Value.Symbol, rightExponent);

            // shrink left cell now that we've split it
            _currentNode.Value.Exponent = leftExponent;

            var nodeCellSmall = _tape.AddAfter(_currentNode, newCellSmall);
            var nodeCellRemainder = _tape.AddAfter(nodeCellSmall, newCellRemainder);

            _currentNode = nodeCellSmall;

            // We're in the new cell, which only has one value, so set offset to the only value.
            _headExponent = 1;
        }

        /// <summary>
        /// This is called to write a symbol to the end of Node.
        /// The node has Exponent > 1.
        /// The adjacent nodes, if they exist, have different symbols.
        /// </summary>
        /// <param name="symbol"></param>
        private void Write_Last_Solo(byte symbol)
        {
            _currentNode.Value.Exponent = _currentNode.Value.Exponent - 1;
            _headExponent = 1;

            var newCell = new SmallExponentTapeCell(symbol, 1);
            var newNode = _tape.AddAfter(_currentNode, newCell);
            _currentNode = newNode;
        }

        /// <summary>
        /// This is called when writing a symbol to the start
        /// of a node, and the previous node has the same symbol
        /// as being written.
        /// The node has Exponent > 1.
        /// </summary>
        /// <param name="symbol"></param>
        private void Write_IntoFirst(byte symbol)
        {
            _currentNode.Value.Exponent = _currentNode.Value.Exponent - 1;
            _currentNode.Previous.Value.Exponent = _currentNode.Previous.Value.Exponent + 1;
            _headExponent = _currentNode.Previous.Value.Exponent;
            _currentNode = _currentNode.Previous;
        }

        /// <summary>
        /// This is called when writing a symbol to the end of
        /// a node, and the next node has the same symbol as being
        /// written.
        /// The node has Exponent > 1.
        /// </summary>
        /// <param name="symbol"></param>
        private void Write_IntoLast(byte symbol)
        {
            _currentNode.Value.Exponent = _currentNode.Value.Exponent - 1;
            _currentNode.Next.Value.Exponent = _currentNode.Next.Value.Exponent + 1;
            _headExponent = 1;
            _currentNode = _currentNode.Next;
        }
    }
}
