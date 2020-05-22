using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WebLab7_1.Models;

namespace FlowGenerator
{
    public class FlowGeneratorC
    {
        private Random random;
        private int m_sum;
        private int m_maxEdge;
        private int m_minEdge;

        public FlowGeneratorC()
        {
            this.random = new Random();
            this.m_sum = 15;
            this.m_maxEdge = 8;
            this.m_minEdge = 2;
        }

        public FlowGeneratorC(int m_sum, int m_maxEdge, int m_minEdge)
        {
            this.random = new Random();
            this.m_sum = m_sum;
            this.m_maxEdge = m_maxEdge;
            this.m_minEdge = m_minEdge;
        }

        public Flow[] generateNodes(int nodeCount)
        {
            Flow[][] nodes = new Flow[nodeCount][];
            int lastFlowId = 0;

            nodes[0] = generateFlowsOfNode(-1, 1, 2, ref lastFlowId, nodes);

            for (int i = 1; i < nodeCount - 1; i++)
            {
                nodes[i] = generateFlowsOfNode(i, i+1, i+2, ref lastFlowId, nodes);
            }

            nodes[nodeCount - 1] = generateFlowsOfNode(nodeCount - 1, nodeCount, nodeCount + 1, ref lastFlowId, nodes);

            return nodes.SelectMany(x => x).ToArray();
        }

        public Flow[] generateFlowsOfNode(int prevNodeId, int curNodeId, int nextNodeId, ref int lastFlowId, Flow[][] nodes)
        {
            List<Flow> flows = new List<Flow>();

            List<double> flowsWeightsIn = new List<double>();
            List<double> flowsWeightsOut = new List<double>();

            bool prevNodeIdUsed = false;
            bool nextNodeIdUsed = false;

            double bridgeFlowWeight = 0;

            if (prevNodeId != -1)
            {
                Flow[] preNode = nodes[prevNodeId - 1];
                Flow bridgeFlow = Array.Find(preNode, (flow) => flow.DestId > 0 && flow.SourceId > 0);
                prevNodeIdUsed = true;
                bridgeFlowWeight = bridgeFlow.X0;
            }

            generateFlowsWeights(this.m_sum - bridgeFlowWeight, this.m_maxEdge, this.m_minEdge, flowsWeightsIn);
            generateFlowsWeights(this.m_sum, this.m_maxEdge, this.m_minEdge, flowsWeightsOut);

            double[] tolerance = new double[flowsWeightsIn.Count + flowsWeightsOut.Count];
            for (int j = 0; j < tolerance.Length; j++)
            {
                tolerance[j] = random.NextDouble();
            }

            int i = 0;
            foreach(double weight in flowsWeightsIn)
            {
                Flow flow = new Flow();

                if (!prevNodeIdUsed)
                {
                    flow.SourceId = prevNodeId;
                    prevNodeIdUsed = true;
                }
                else
                {
                    flow.SourceId = -1;
                }

                flow.DestId = curNodeId;
                flow.Tolerance = tolerance[i];
                flow.X0 = weight;
                flow.UpperBound = -1;
                flow.LowerBound = -1;
                flow.Id = ++lastFlowId;

                flows.Add(flow);
                i++;
            }

            foreach (double weight in flowsWeightsOut)
            {
                Flow flow = new Flow();

                if (!nextNodeIdUsed)
                {
                    flow.DestId = nextNodeId;
                    nextNodeIdUsed = true;
                }
                else
                {
                    flow.DestId = -1;
                }

                flow.SourceId = curNodeId;
                flow.Tolerance = tolerance[i];
                flow.X0 = weight;
                flow.Id = ++lastFlowId;

                flows.Add(flow);
                i++;
            }

            return flows.ToArray();
        }

        private void generateFlowsWeights(double number, int maxEdge, int minEdge, List<double> flowsWeights)
        {
            if (number <= minEdge)
            {
                flowsWeights.Add(number);
                return;
            }

            double rndNumber = random.NextDouble() * maxEdge;
            flowsWeights.Add(rndNumber);
            generateFlowsWeights(number - rndNumber, maxEdge, minEdge, flowsWeights);
        }
    }
}
