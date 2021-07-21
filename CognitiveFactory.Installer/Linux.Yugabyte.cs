using System;

namespace CognitiveFactory.Installer.Linux
{
    class Yugabyte
    {
        // Executes : helm install yugabytedb/yugabyte
        // Returns : 
        // null if install was successful
        // diagnostic string if one of the commands failed
        public static string InstallYugabyteDB(string installNamespace="cogfactory-db", string installName="cogfactory-db", 
            string masterCpuRequest="0.5", string masterMemoryRequest= "0.5Gi", string masterStorageClass="local-path",
            string tserverCpuRequest = "0.5", string tserverMemoryRequest = "0.5Gi", string tserverStorageClass="local-path" )
        {
            // https://docs.yugabyte.com/latest/deploy/kubernetes/single-zone/oss/helm-chart/

            // 1. helm repo add yugabytedb https://charts.yugabyte.com
            try
            {
                string output;
                string error;
                int exitcode = Process.Run("helm", "repo add yugabytedb https://charts.yugabyte.com", 5, out output, out error);
                if (exitcode != 0 || !String.IsNullOrEmpty(error))
                {
                    return $"helm repo add : exitcode={exitcode}, output=\"{output}\", error=\"{error}\"";
                }
            }
            catch(Exception e)
            {
                return $"helm repo add : exception=\"{e.Message}\"";
            }

            // 2. helm repo update
            try
            {
                string output;
                string error;
                int exitcode = Process.Run("helm", "repo update", 30, out output, out error);
                if (exitcode != 0 || !String.IsNullOrEmpty(error))
                {
                    return $"helm repo update : exitcode={exitcode}, output=\"{output}\", error=\"{error}\"";
                }
            }
            catch (Exception e)
            {
                return $"helm repo update : exception=\"{e.Message}\"";
            }

            // 3. kubectl create namespace cogfactory-db
            try
            {
                string output;
                string error;
                int exitcode = Process.Run("kubectl", $"create namespace {installNamespace}", 5, out output, out error);
                if (!(!String.IsNullOrEmpty(error) && error.Contains("AlreadyExists")) && (exitcode != 0 || !String.IsNullOrEmpty(error)))
                {
                    return $"kubectl create namespace : exitcode={exitcode}, output=\"{output}\", error=\"{error}\"";
                }
            }
            catch (Exception e)
            {
                return $"kubectl create namespace : exception=\"{e.Message}\"";
            }

            // 4. helm install yugabytedb/yugabyte
            try
            {
                string output;
                string error;
                string args = $"install {installName} yugabytedb/yugabyte --namespace {installNamespace}";
                args += $" --set resource.master.requests.cpu={masterCpuRequest},resource.master.requests.memory={masterMemoryRequest},storage.master.storageClass={masterStorageClass},";
                args += $"resource.tserver.requests.cpu={tserverCpuRequest},resource.tserver.requests.memory={tserverMemoryRequest},storage.tserver.storageClass={tserverStorageClass},";
                args += "nodeSelector.disk=local";
                args += " --wait --timeout 30m";
                int exitcode = Process.Run("helm", args, 1800, out output, out error);
                if (exitcode != 0)
                {
                    return $"helm install yugabytedb/yugabyte : exitcode={exitcode}, output=\"{output}\", error=\"{error}\"";
                }
            }
            catch (Exception e)
            {
                return $"helm install yugabytedb/yugabyte : exception=\"{e.Message}\"";
            }
            return null;

            /* Then check install  / upgrade / uninstall / delete PVC :

            kubectl get pvc,po,sts,svc -n cogfactory-db

            helm status cogfactory-db -n cogfactory-db

            PGSQL connection : Host=localhost;Port=5433;Database=yugabyte;Username=yugabyte;Password=yugabyte

            helm upgrade cogfactory-db yugabytedb/yugabyte --set Image.tag= 2.1.6.0-b17 --wait -n cogfactory-db

            helm uninstall cogfactory-db -n cogfactory-db

            kubectl delete pvc --namespace cogfactory-db --all
            */
        }
    }
}
